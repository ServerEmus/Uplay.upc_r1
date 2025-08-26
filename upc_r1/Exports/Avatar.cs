using StbImageSharp;

namespace upc_r1.Exports;

internal static class Avatar
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_AVATAR_GetBitmap", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_AVATAR_GetBitmap(IntPtr AvatarId, int AvatarSize, IntPtr OutRGBA, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {AvatarId} {AvatarSize} {OutRGBA} {Overlapped}", nameof(UPLAY_AVATAR_GetBitmap), AvatarId, AvatarSize, OutRGBA, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_AVATAR_GetAvatarIdForCurrentUser", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_AVATAR_GetAvatarIdForCurrentUser(IntPtr OutAvatarId, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {OutAvatarId} {Overlapped}", nameof(UPLAY_AVATAR_GetAvatarIdForCurrentUser), OutAvatarId, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_AVATAR_Get", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_AVATAR_Get(IntPtr AccountIdUtf8, int AvatarSize, IntPtr OutRGBA, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {AvatarId} {AvatarSize} {OutRGBA} {Overlapped}", nameof(UPLAY_AVATAR_GetBitmap), AccountIdUtf8, AvatarSize, OutRGBA, Overlapped);
        if (string.IsNullOrEmpty(UPC_Json.Instance.AvatarsPath))
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
            return false;
        }
        string? accountid = Marshal.PtrToStringAnsi(AccountIdUtf8);
        if (string.IsNullOrEmpty(accountid))
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
            return false;
        }
        Uplay.Uplaydll.AvatarSize size = (Uplay.Uplaydll.AvatarSize)AvatarSize;
        string sizeStr = size switch
        {
            Uplay.Uplaydll.AvatarSize._64 => "64",
            Uplay.Uplaydll.AvatarSize._128 => "128",
            Uplay.Uplaydll.AvatarSize._256 => "256",
            _ => "64",
        };
        string path = Path.Combine(UPC_Json.Instance.AvatarsPath, $"{accountid}_{sizeStr}.png");
        if (!File.Exists(path))
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.Failed);
            return false;
        }
        using var stream = File.OpenRead(path);
        ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        byte[] data = image.Data;
        // Convert rgba to bgra
        for (int i = 0; i < image.Width * image.Width; ++i)
        {
            byte r = data[i * 4];
            byte g = data[i * 4 + 1];
            byte b = data[i * 4 + 2];
            byte a = data[i * 4 + 3];


            data[i * 4] = b;
            data[i * 4 + 1] = g;
            data[i * 4 + 2] = r;
            data[i * 4 + 3] = a;
        }
        Marshal.Copy(data, 0, OutRGBA, data.Length);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_AVATAR_Release", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_AVATAR_Release(IntPtr RGBA)
    {
        Log.Verbose("[{Function}] {RGBA}", nameof(UPLAY_AVATAR_Release), RGBA);
        Marshal.FreeHGlobal(RGBA);
        return true;
    }
}
