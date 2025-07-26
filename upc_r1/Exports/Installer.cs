namespace upc_r1.Exports;

public static class Installer
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_AreChunksInstalled", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_AreChunksInstalled(IntPtr ChunkIds, uint ChunkCount)
    {
        Log.Verbose("[{Function}] {ChunkIdPtr} {ChunkCount}", nameof(UPLAY_INSTALLER_AreChunksInstalled), ChunkIds, ChunkCount);
        // All chunks are installed
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetChunkIdsFromTag", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_GetChunkIdsFromTag(IntPtr TagUtf8, IntPtr OutChunkIdList)
    {
        string? tag = Marshal.PtrToStringAnsi(TagUtf8);
        Log.Verbose("[{Function}] {TagUtf8} {OutChunkIdList}", nameof(UPLAY_INSTALLER_GetChunkIdsFromTag), TagUtf8, OutChunkIdList);
        List<uint> ChunkIds = UPC_Json.Instance.ChunkInfo.ChunkIds;
        WriteOutList(OutChunkIdList, ChunkIds);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetChunks", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_GetChunks(IntPtr OutChunkIdList)
    {
        Log.Verbose("[{Function}] {OutChunkIdList}", nameof(UPLAY_INSTALLER_GetChunks), OutChunkIdList);
        List<uint> ChunkIds = UPC_Json.Instance.ChunkInfo.ChunkIds;
        WriteOutList(OutChunkIdList, ChunkIds);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetLanguageUtf8", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPLAY_INSTALLER_GetLanguageUtf8()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_INSTALLER_GetLanguageUtf8));
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Country);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_Init(uint Flags)
    {
        Log.Verbose("[{Function}] {Flags}", nameof(UPLAY_INSTALLER_Init), Flags);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_ReleaseChunkIdList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_ReleaseChunkIdList(IntPtr ChunkIdList)
    {
        Log.Verbose("[{Function}] {Flags}", nameof(UPLAY_INSTALLER_ReleaseChunkIdList), ChunkIdList);
        FreeList(ChunkIdList);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_UpdateInstallOrder", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_UpdateInstallOrder(IntPtr ChunkIds, uint ChunkCount)
    {
        Log.Verbose("[{Function}] {ChunkIdPtr} {ChunkCount}", nameof(UPLAY_INSTALLER_UpdateInstallOrder), ChunkIds, ChunkCount);
        return true;
    }
}
