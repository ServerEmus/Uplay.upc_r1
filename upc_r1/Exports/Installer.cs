namespace upc_r1.Exports;

public static class Installer
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_AreChunksInstalled", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_AreChunksInstalled(IntPtr ChunkIds, uint ChunkCount)
    {
        Log.Information("[{Function}] {ChunkIdPtr} {ChunkCount}", nameof(UPLAY_INSTALLER_AreChunksInstalled), ChunkIds, ChunkCount);
        // All chunks are installed
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetChunkIdsFromTag", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_GetChunkIdsFromTag(IntPtr TagUtf8, IntPtr OutChunkIdList)
    {
        string? tag = Marshal.PtrToStringAnsi(TagUtf8);
        Log.Information(nameof(UPLAY_INSTALLER_GetChunkIdsFromTag), [TagUtf8, OutChunkIdList]);
        List<uint> ChunkIds = UPC_Json.Instance.ChunkInfo.ChunkIds;
        WriteOutList(OutChunkIdList, ChunkIds);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetChunks", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_GetChunks(IntPtr OutChunkIdList)
    {
        Log.Information(nameof(UPLAY_INSTALLER_GetChunks), [OutChunkIdList]);
        List<uint> ChunkIds = UPC_Json.Instance.ChunkInfo.ChunkIds;
        WriteOutList(OutChunkIdList, ChunkIds);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_GetLanguageUtf8", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPLAY_INSTALLER_GetLanguageUtf8()
    {
        Log.Information(nameof(UPLAY_INSTALLER_GetLanguageUtf8), []);
        return Marshal.StringToHGlobalAnsi(UPC_Json.Instance.Account.Country);
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_Init(uint aFlags)
    {
        Log.Information(nameof(UPLAY_INSTALLER_Init), [aFlags]);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_ReleaseChunkIdList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_ReleaseChunkIdList(IntPtr ChunkIdList)
    {
        Log.Information(nameof(UPLAY_INSTALLER_ReleaseChunkIdList), [ChunkIdList]);
        DllShared.MarshalHelper.FreeList(ChunkIdList);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_INSTALLER_UpdateInstallOrder", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_INSTALLER_UpdateInstallOrder(IntPtr ChunkIds, uint ChunkCount)
    {
        Log.Information(nameof(UPLAY_INSTALLER_UpdateInstallOrder), [ChunkIds, ChunkCount]);
        return true;
    }
}
