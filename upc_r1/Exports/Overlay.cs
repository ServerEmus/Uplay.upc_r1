namespace upc_r1.Exports;

internal class Overlay
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_SetShopUrl", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_SetShopUrl(IntPtr Url, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {Url} {Overlapped}", nameof(UPLAY_OVERLAY_SetShopUrl), Marshal.PtrToStringAnsi(Url), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_Show", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_Show(int OverlaySection, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {Url} {Overlapped}", nameof(UPLAY_OVERLAY_SetShopUrl), OverlaySection, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_ShowBrowserUrl", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_ShowBrowserUrl(IntPtr UrlUtf8)
    {
        Log.Information("[{Function}] {Url}", nameof(UPLAY_OVERLAY_ShowBrowserUrl), Marshal.PtrToStringAnsi(UrlUtf8));
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_ShowFacebookAuthentication", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_ShowFacebookAuthentication(IntPtr FacebookAppId, IntPtr RedirectUri, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {FacebookAppId} {RedirectUri} {Overlapped}", nameof(UPLAY_OVERLAY_ShowShopUrl), Marshal.PtrToStringAnsi(FacebookAppId), Marshal.PtrToStringAnsi(RedirectUri), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_ShowMicroApp", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_ShowMicroApp(IntPtr AppName, IntPtr MicroAppParamList, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AppName} {MicroAppParamList} {Overlapped}", nameof(UPLAY_OVERLAY_ShowShopUrl), Marshal.PtrToStringAnsi(AppName), MicroAppParamList, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_ShowNotification", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_ShowNotification(uint NotificationId)
    {
        Log.Information("[{Function}] {NotificationId}", nameof(UPLAY_OVERLAY_ShowNotification), NotificationId);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_OVERLAY_ShowShopUrl", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_OVERLAY_ShowShopUrl(IntPtr UrlUtf8)
    {
        Log.Information("[{Function}] {Url}", nameof(UPLAY_OVERLAY_ShowShopUrl), Marshal.PtrToStringAnsi(UrlUtf8));
        return false;
    }
}
