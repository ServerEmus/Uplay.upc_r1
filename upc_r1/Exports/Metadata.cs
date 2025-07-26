namespace upc_r1.Exports;

internal class Metadata
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_METADATA_ClearContinuousTag", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_METADATA_ClearContinuousTag(IntPtr StringNameUtf8)
    {
        Log.Verbose("[{Function}] {StringName}", nameof(UPLAY_METADATA_ClearContinuousTag), Marshal.PtrToStringAnsi(StringNameUtf8));
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_METADATA_SetContinuousTag", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_METADATA_SetContinuousTag(IntPtr StringNameUtf8, IntPtr StringValueUtf8)
    {
        Log.Verbose("[{Function}] {StringName} {StringValue}", nameof(UPLAY_METADATA_SetContinuousTag), Marshal.PtrToStringAnsi(StringNameUtf8), Marshal.PtrToStringAnsi(StringValueUtf8));
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_METADATA_SetSingleEventTag", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_METADATA_SetSingleEventTag(IntPtr StringNameUtf8, IntPtr StringValueUtf8)
    {
        Log.Verbose("[{Function}] {StringName} {StringValue}", nameof(UPLAY_METADATA_SetSingleEventTag), Marshal.PtrToStringAnsi(StringNameUtf8), Marshal.PtrToStringAnsi(StringValueUtf8));
        return true;
    }
}
