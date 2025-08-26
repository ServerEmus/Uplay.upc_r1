namespace upc_r1.Exports;

internal class SaveSlot
{
    public uint Mode { get; set; }          // +0: 0=read, 1=write
    public uint SlotId { get; set; }        // +4: slot identifier  
    public ulong HeaderSize { get; set; }   // +8: 552 bytes
    public FileStream? FileHandle { get; set; } // +16: file stream
    public string? SaveName { get; set; }   // +24: save name for header

    public SaveSlot()
    {
        HeaderSize = 552;
    }
}

internal class Save
{
    static readonly SaveSlot?[] SaveSlots = new SaveSlot[256];

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Close", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Close(IntPtr SlotId)
    {
        Log.Verbose("[{Function}] {SlotId}", nameof(UPLAY_SAVE_Close), SlotId);
        if (SlotId < 0 || SlotId >= SaveSlots.Length)
            return false;

        var slot = SaveSlots[SlotId];
        if (slot == null)
            return false;
        // If mode was 1 (write), write the formatted header
        if (slot.Mode == 1)
        {
            // Set default name if not provided
            slot.SaveName ??= "Unnamed";

            // Build file path
            string baseSavePath = UPC_Json.Instance.Save.Path;
            string savePath = UPC_Json.Instance.Save.UseAppIdInName
                ? Path.Combine(baseSavePath, Main.ProductId.ToString(), $"{SlotId}.save")
                : Path.Combine(baseSavePath, $"{SlotId}.save");

            try
            {
                using var fileStream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                fileStream.Seek(0, SeekOrigin.Begin);

                // Create 552-byte header
                byte[] header = new byte[552];

                // Write header size - 4 at offset 0
                uint headerSizeValue = (uint)(slot.HeaderSize - 4);
                BitConverter.GetBytes(headerSizeValue).CopyTo(header, 0);

                // Write save name as Unicode at offset 40
                byte[] nameBytes = System.Text.Encoding.Unicode.GetBytes(slot.SaveName);
                int maxNameBytes = Math.Min(nameBytes.Length, 552 - 40);
                Array.Copy(nameBytes, 0, header, 40, maxNameBytes);

                fileStream.Write(header);
                fileStream.Flush();
            }
            catch (Exception ex)
            {
                Log.Verbose("[{Function}] Header write failed: {Message}", nameof(UPLAY_SAVE_Close),ex.Message);
            }
        }
        
        // Close file handle if it exists
        slot.FileHandle?.Dispose();

        // Clear slot
        SaveSlots[SlotId] = null;
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_GetSavegames", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_GetSavegames(IntPtr OutGamesList, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {OutGamesList} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), OutGamesList, Overlapped);
        if (OutGamesList == IntPtr.Zero)
            return false;
        string baseSavePath = UPC_Json.Instance.Save.Path;
        string savePath = UPC_Json.Instance.Save.UseAppIdInName
            ? Path.Combine(baseSavePath, Main.ProductId.ToString())
            : baseSavePath;
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
        ReadOnlySpan<string> files = Directory.GetFiles(savePath, "*.save");
        List<UPLAY_SAVE_Game> saves = [];
        foreach (var item in files)
        {
            if (string.IsNullOrEmpty(item))
                continue;
            FileInfo info = new(item);

            string filenameWithoutExt = Path.GetFileNameWithoutExtension(info.Name);

            if (!ulong.TryParse(filenameWithoutExt, out ulong fileId))
                continue;
            string saveName = "Unnamed";
            try
            {
                using FileStream fileStream = new(item, FileMode.Open, FileAccess.Read, FileShare.Read);
                if (fileStream.Length >= 552)
                {
                    byte[] header = new byte[552];
                    fileStream.ReadExactly(header, 0, 552);

                    // Extract Unicode save name from offset 40
                    List<byte> nameBytes = [];
                    for (int i = 40; i < 551; i += 2)
                    {
                        byte low = header[i];
                        byte high = header[i + 1];
                        if (low == 0 && high == 0) break;
                        nameBytes.Add(low);
                        nameBytes.Add(high);
                    }

                    if (nameBytes.Count > 0)
                        saveName = System.Text.Encoding.Unicode.GetString([.. nameBytes]);
                }
            }
            catch (Exception ex)
            {
                Log.Verbose("[{Function}] Header read failed: {Message}", nameof(UPLAY_SAVE_GetSavegames), ex.Message);
            }
            saves.Add(new()
            {
                nameUtf8 = saveName,
                id = fileId,
                size = (ulong)(info.Length - 552)
            });
        }
        WriteOutList(OutGamesList, saves);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Open", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Open(uint SlotId, uint Mode, IntPtr OutSaveHandle, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SlotId} {Mode} {OutSaveHandle} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), SlotId, Mode, OutSaveHandle, Overlapped);
        if (SlotId >= SaveSlots.Length)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.InvalidArgument);
            return false;
        }

        SaveSlots[SlotId] = new()
        { 
            Mode = Mode,
            SlotId = SlotId,
            HeaderSize = 552,
            FileHandle = null,
            SaveName = null
        };
        string baseSavePath = UPC_Json.Instance.Save.Path;
        string savePath = UPC_Json.Instance.Save.UseAppIdInName
            ? Path.Combine(baseSavePath, Main.ProductId.ToString(), $"{SlotId}.save")
            : Path.Combine(baseSavePath, $"{SlotId}.save");
        Log.Verbose("[{Function}] Save Path: {Path}", nameof(UPLAY_SAVE_GetSavegames), savePath);
        if (!Directory.Exists(Path.GetDirectoryName(savePath)))
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
        if (!File.Exists(savePath))
            File.Create(savePath).Close();

        FileStream? fileStream = null;

        if (Mode == 0) // Read mode
        {
            if (!File.Exists(savePath))
            {
                Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
                return false;
            }

            try
            {
                fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                Log.Verbose("[{Function}] Failed to open for read: {Message}", nameof(UPLAY_SAVE_Open), ex.Message);
                Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
                return false;
            }
        }
        else if (Mode == 1) // Write mode
        {
            if (!File.Exists(savePath))
            {
                try
                {
                    // Create file with initial 552-byte header
                    using var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
                    byte[] header = new byte[552];
                    fs.Write(header);
                }
                catch (Exception ex)
                {
                    Log.Verbose("[{Function}] Failed to create file: {Message}", nameof(UPLAY_SAVE_Open), ex.Message);
                    Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
                    return false;
                }
            }
            fileStream = null; // Don't keep stream open for write mode
        }

        SaveSlots[SlotId]!.FileHandle = fileStream;

        Marshal.WriteInt32(OutSaveHandle, 0, (int)SlotId);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Read", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Read(int SlotId, uint NumOfBytesToRead, uint Offset, IntPtr OutBuffer, IntPtr OutNumOfBytesRead, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SaveHandle} {NumOfBytesToRead} {Offset} {OutBuffer} {OutNumOfBytesRead} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), SlotId, NumOfBytesToRead, Offset, OutBuffer, OutNumOfBytesRead, Overlapped);


        if (SlotId < 0 || SlotId >= SaveSlots.Length)
        {
            Marshal.WriteInt32(OutNumOfBytesRead, 0);
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        }
        string baseSavePath = UPC_Json.Instance.Save.Path;
        string savePath = UPC_Json.Instance.Save.UseAppIdInName
            ? Path.Combine(baseSavePath, Main.ProductId.ToString(), $"{SlotId}.save")
            : Path.Combine(baseSavePath, $"{SlotId}.save");

        IntPtr actualBuffer = Marshal.ReadIntPtr(OutBuffer);
        bool success = false;
        int bytesRead = 0;

        try
        {
            using var fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            long seekPosition = Offset + 552; // Skip header
            fileStream.Seek(seekPosition, SeekOrigin.Begin);

            byte[] tempBuffer = new byte[NumOfBytesToRead];
            bytesRead = fileStream.Read(tempBuffer, 0, (int)NumOfBytesToRead);

            Marshal.WriteInt32(OutNumOfBytesRead, bytesRead);

            if (actualBuffer != IntPtr.Zero && bytesRead > 0)
                Marshal.Copy(tempBuffer, 0, actualBuffer, bytesRead);

            success = bytesRead > 0;
        }
        catch (Exception ex)
        {
            Log.Verbose("[{Function}] Read failed: {Message}", nameof(UPLAY_SAVE_Read), ex.Message);
            Marshal.WriteInt32(OutNumOfBytesRead, 0);
        }
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        return success;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_ReleaseGameList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_ReleaseGameList(IntPtr GamesList)
    {
        Log.Verbose("[{Function}] {GamesList}", nameof(UPLAY_SAVE_ReleaseGameList), GamesList);
        if (GamesList == IntPtr.Zero)
            return false;
        FreeList(GamesList);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Remove", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Remove(uint SlotId, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SlotId} {Overlapped}", nameof(UPLAY_SAVE_Remove), SlotId, Overlapped);
        string baseSavePath = UPC_Json.Instance.Save.Path;
        string savePath = UPC_Json.Instance.Save.UseAppIdInName
            ? Path.Combine(baseSavePath, Main.ProductId.ToString(), $"{SlotId}.save")
            : Path.Combine(baseSavePath, $"{SlotId}.save");

        try
        {
            if (File.Exists(savePath))
                File.Delete(savePath);

            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
            return true;
        }
        catch (Exception ex)
        {
            Log.Verbose("[{Function}] Remove failed: {Message}", nameof(UPLAY_SAVE_Remove), ex.Message);
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Failed);
            return false;
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_SetName", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_SetName(uint SlotId, IntPtr NameUtf8)
    {
        Log.Verbose("[{Function}] {SlotId} {NameUtf8}", nameof(UPLAY_SAVE_SetName), SlotId, NameUtf8);
        string? nameUtf = Marshal.PtrToStringAnsi(NameUtf8);
        SaveSlots[SlotId]!.SaveName = nameUtf;
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Write", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Write(uint SlotId, uint NumOfBytesToWrite, IntPtr Buffer, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SaveHandle} {NumOfBytesToWrite} {Buffer} {Overlapped}", nameof(UPLAY_SAVE_Write), SlotId, NumOfBytesToWrite, Buffer, Overlapped);
        if (SlotId >= SaveSlots.Length)
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
            return false;
        }
        var slot = SaveSlots[SlotId];
        if (slot == null)
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
            return false;
        }
        // Build file path
        string baseSavePath = UPC_Json.Instance.Save.Path;
        string savePath = UPC_Json.Instance.Save.UseAppIdInName
            ? Path.Combine(baseSavePath, Main.ProductId.ToString(), $"{SlotId}.save")
            : Path.Combine(baseSavePath, $"{SlotId}.save");

        IntPtr actualBuffer = Marshal.ReadIntPtr(Buffer);
        if (actualBuffer == IntPtr.Zero)
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
            return false;
        }

        bool success = false;
        try
        {
            byte[] buffer = new byte[NumOfBytesToWrite];
            Marshal.Copy(actualBuffer, buffer, 0, (int)NumOfBytesToWrite);

            using var fileStream = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileStream.Seek(552, SeekOrigin.Begin);
            fileStream.Write(buffer);

            // Truncate to exact size to match original DLL behavior
            fileStream.SetLength(552 + buffer.Length);
            fileStream.Flush(true);

            success = true;
            var uploadPath = savePath + ".upload";
            try
            {
                using var uploadStream = new FileStream(uploadPath, FileMode.Create, FileAccess.Write);
                // Original just creates/opens the upload file, content might be empty or minimal
                uploadStream.Flush(true);
                Log.Verbose("[{Function}] Created upload file: {uploadPath}", nameof(UPLAY_SAVE_Remove), uploadPath);
            }
            catch (Exception uploadEx)
            {
                Log.Verbose("[{Function}] Failed to create upload file: {Message}", nameof(UPLAY_SAVE_Remove), uploadEx.Message);
                Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Failed);
                return false;
            }
        }
        catch (Exception ex)
        {
            Log.Verbose("[{Function}] Write failed: {Message}", nameof(UPLAY_SAVE_Remove), ex.Message);
        }

        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        return success;
    }
}
