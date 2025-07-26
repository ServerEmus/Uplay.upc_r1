namespace upc_r1.Exports;

/// <summary>
/// We disable store.
/// </summary>
internal class Store
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_Checkout", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_Checkout(uint Id)
    {
        Log.Verbose("[{Function}] {Id}", nameof(UPLAY_STORE_Checkout), Id);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_GetPartner", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPLAY_STORE_GetPartner()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_STORE_GetPartner));
        return (int)Uplay.Uplaydll.TargetPartner.None;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_GetProducts", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_GetProducts(IntPtr Overlapped, IntPtr OutProductList)
    {
        Log.Verbose("[{Function}] {Overlapped} {OutProductList}", nameof(UPLAY_STORE_Checkout), Overlapped, OutProductList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_IsEnabled", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_IsEnabled()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_STORE_IsEnabled));
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_ReleaseProductsList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_ReleaseProductsList(IntPtr ProductList)
    {
        Log.Verbose("[{Function}] {ProductList}", nameof(UPLAY_STORE_ReleaseProductsList), ProductList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_ShowProductDetails", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_ShowProductDetails(uint Id)
    {
        Log.Verbose("[{Function}] {Id}", nameof(UPLAY_STORE_ShowProductDetails), Id);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_STORE_ShowProducts", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_STORE_ShowProducts(IntPtr Tags)
    {
        Log.Verbose("[{Function}] {Tags}", nameof(UPLAY_STORE_ShowProducts), Tags);
        return false;
    }
}
