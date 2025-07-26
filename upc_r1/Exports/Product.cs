namespace upc_r1.Exports;

internal class Product
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PRODUCT_GetProductList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PRODUCT_GetProductList(IntPtr Overlapped, IntPtr OutProductList)
    {
        Log.Information("[{Function}] {Overlapped} {OutProductList}", nameof(UPLAY_PRODUCT_GetProductList), Overlapped, OutProductList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PRODUCT_ReleaseProductList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PRODUCT_ReleaseProductList(IntPtr ProductList)
    {
        Log.Information("[{Function}] {ProductList}", nameof(UPLAY_PRODUCT_GetProductList), ProductList);
        return false;
    }
}
