using System.Runtime.InteropServices;

namespace upc_r1.Exports;

internal class Save
{
    static readonly Dictionary<int, string> PtrToFilePath = [];

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Close", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Close(IntPtr SaveHandle)
    {
        Log.Verbose("[{Function}] {SaveHandle}", nameof(UPLAY_SAVE_Close), SaveHandle);
        PtrToFilePath.Remove(SaveHandle.ToInt32());
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_GetSavegames", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_GetSavegames(IntPtr OutGamesList, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {OutGamesList} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), OutGamesList, Overlapped);
        if (OutGamesList == IntPtr.Zero)
            return false;
        string savepath = UPC_Json.Instance.Save.Path;
        if (!Directory.Exists(savepath))
            Directory.CreateDirectory(savepath);
        List<UPLAY_SAVE_Game> saves = [];
        uint i = 1;
        foreach (var item in Directory.GetFiles(savepath))
        {
            if (string.IsNullOrEmpty(item))
                continue;

            FileInfo info = new(item);
            UPLAY_SAVE_Game saveGame = new()
            {
                nameUtf8 = info.Name,
                id = i,
                size = (uint)info.Length
            };
            saves.Add(saveGame);
            i++;
        }
        WriteOutList(OutGamesList, saves);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Open", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Open(uint SlotId, uint Mode, IntPtr OutSaveHandle, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SlotId} {Mode} {OutSaveHandle} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), SlotId, Mode, OutSaveHandle, Overlapped);
        string jsonSavePath = UPC_Json.Instance.Save.Path;
        string savePath = string.Empty;
        if (UPC_Json.Instance.Save.UseAppIdInName)
            savePath = Path.Combine(jsonSavePath, Main.ProductId.ToString(), $"{SlotId}.save");
        else
            savePath = Path.Combine(jsonSavePath, $"{SlotId}.save");
        Log.Verbose("[{Function}] Save Path: {Path}", nameof(UPLAY_SAVE_GetSavegames), savePath);
        if (!Directory.Exists(Path.GetDirectoryName(savePath)))
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
        if (!File.Exists(savePath))
            File.Create(savePath).Close();
        int ptr = Random.Shared.Next();
        PtrToFilePath.Add(ptr, savePath);
        Marshal.WriteInt32(OutSaveHandle, 0, ptr);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Read", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Read(IntPtr SaveHandle, uint NumOfBytesToRead, uint Offset, IntPtr OutBuffer, IntPtr OutNumOfBytesRead, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SaveHandle} {NumOfBytesToRead} {Offset} {OutBuffer} {OutNumOfBytesRead} {Overlapped}", nameof(UPLAY_SAVE_GetSavegames), SaveHandle, NumOfBytesToRead, Offset, OutBuffer, OutNumOfBytesRead, Overlapped);
        if (SaveHandle == 0)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_InvalidArgument);
            return false;
        }
        if (!PtrToFilePath.TryGetValue(SaveHandle.ToInt32(), out string? path))
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_InvalidArgument);
            return false;
        }
        if (path == null)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_InvalidArgument);
            return false;
        }
        if (NumOfBytesToRead <= 0)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_InvalidArgument);
            return false;
        }
        FileStream filestream = File.OpenRead(path);
        var buff = new byte[NumOfBytesToRead];
        var readed = filestream.Read(buff, (int)Offset, (int)NumOfBytesToRead);
        filestream.Close();
        if (readed < 0)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_InvalidArgument);
            return false;
        }
        Marshal.WriteInt32(OutNumOfBytesRead, readed);
        Marshal.Copy(buff, 0, OutBuffer, buff.Length);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
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
        string savepath = UPC_Json.Instance.Save.Path;
        if (!Directory.Exists(savepath))
            Directory.CreateDirectory(savepath);
        if (UPC_Json.Instance.Save.UseAppIdInName)
            savepath = Path.Combine(savepath, Main.ProductId.ToString(), $"{SlotId}.save");
        var files = Directory.GetFiles(savepath);
        if (files.Length > SlotId - 1)
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Failed);
            return false;
        }
        var file = files.ElementAt((int)SlotId - 1);
        File.Delete(file);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_SetName", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_SetName(IntPtr SaveHandle, IntPtr NameUtf8)
    {
        Log.Verbose("[{Function}] {SaveHandle} {NameUtf8}", nameof(UPLAY_SAVE_SetName), SaveHandle, NameUtf8);
        string? nameUtf = Marshal.PtrToStringAnsi(NameUtf8);
        if (string.IsNullOrEmpty(nameUtf))
            return false;
        if (!PtrToFilePath.TryGetValue(SaveHandle.ToInt32(), out string? path))
            return false;
        if (path == null)
            return false;
        string newFileName = path.Replace(Path.GetFileNameWithoutExtension(path), nameUtf);
        File.Copy(path, newFileName);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SAVE_Write", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SAVE_Write(IntPtr SaveHandle, uint NumOfBytesToWrite, IntPtr Buffer, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {SaveHandle} {NumOfBytesToWrite} {Buffer} {Overlapped}", nameof(UPLAY_SAVE_Write), SaveHandle, NumOfBytesToWrite, Buffer, Overlapped);
        if (!PtrToFilePath.TryGetValue(SaveHandle.ToInt32(), out string? path))
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Failed);
            return false;
        }
        if (path == null)
        {
            Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Failed);
            return false;
        }
        var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        var buff = new byte[NumOfBytesToWrite];
        Marshal.Copy(Buffer, buff, 0, (int)NumOfBytesToWrite);
        stream.Write(buff);
        stream.Flush(true);
        stream.Close();
        return true;
    }
}
