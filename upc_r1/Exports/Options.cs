namespace upc_r1.Exports;

internal class Options
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Apply", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_Apply(IntPtr FileHandle, IntPtr KeyValueList, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {FileHandle} {KeyValueList} {Overlapped}", nameof(UPLAY_OPTIONS_Apply), FileHandle, KeyValueList, Overlapped);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Close", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_Close(IntPtr FileHandle)
    {
        Log.Information("[{Function}] {FileHandle}", nameof(UPLAY_OPTIONS_Close), FileHandle);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Enumerate", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_Enumerate(IntPtr FileHandle, IntPtr OutKeyValueList, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {FileHandle} {KeyValueList} {Overlapped}", nameof(UPLAY_OPTIONS_Enumerate), FileHandle, OutKeyValueList, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Get", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPLAY_OPTIONS_Get(IntPtr KeyValueList, IntPtr Key)
    {
        Log.Information("[{Function}] {KeyValueList} {Key}", nameof(UPLAY_OPTIONS_Get), KeyValueList, Key);
        return IntPtr.Zero;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Open", CallConvs = [typeof(CallConvCdecl)])]
    public static IntPtr UPLAY_OPTIONS_Open(IntPtr Name)
    {
        Log.Information("[{Function}] {Name}", nameof(UPLAY_OPTIONS_Open), Name);
        return IntPtr.Zero;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_ReleaseKeyValueList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_ReleaseKeyValueList(IntPtr KeyValueList)
    {
        Log.Information("[{Function}] {KeyValueList}", nameof(UPLAY_OPTIONS_ReleaseKeyValueList), KeyValueList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_Set", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_Set(IntPtr KeyValueList, IntPtr Key, IntPtr Value)
    {
        Log.Information("[{Function}] {KeyValueList} {Key} {Value}", nameof(UPLAY_OPTIONS_Set), KeyValueList, Key, Value);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OPTIONS_SetInGameState", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OPTIONS_SetInGameState(uint Flags)
    {
        Log.Information("[{Function}] {Flags}", nameof(UPLAY_OPTIONS_SetInGameState), Flags);
        return false;
    }
}
