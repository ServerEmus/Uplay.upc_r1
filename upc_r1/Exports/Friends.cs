namespace upc_r1.Exports;

public class Friends
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_AddPlayedWith", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_AddPlayedWith(IntPtr DescriptionUtf8, IntPtr AccountIdListUtf8, uint AccountIdListLength)
    {
        Log.Information("[{Function}] {DescriptionUtf8} {AccountIdListUtf8} {AccountIdListLength}", nameof(UPLAY_FRIENDS_AddPlayedWith), Marshal.PtrToStringAnsi(DescriptionUtf8), AccountIdListUtf8, AccountIdListLength);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_AddToBlackList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_AddToBlackList(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_FRIENDS_AddPlayedWith), Marshal.PtrToStringAnsi(AccountIdUtf8), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_DisableFriendMenuItem", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_DisableFriendMenuItem(uint Id)
    {
        Log.Information("[{Function}] {Id}", nameof(UPLAY_FRIENDS_DisableFriendMenuItem), Id);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_EnableFriendMenuItem", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_EnableFriendMenuItem(uint Id, uint MenuItemMode, uint Filter)
    {
        Log.Information("[{Function}] {Id} {MenuItemMode} {Filter}", nameof(UPLAY_FRIENDS_EnableFriendMenuItem), Id, MenuItemMode, Filter);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_GetFriendList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_GetFriendList(uint FriendListFilter, IntPtr OutFriendList)
    {
        Log.Information("[{Function}] {FriendListFilter} {OutFriendList}", nameof(UPLAY_FRIENDS_GetFriendList), FriendListFilter, OutFriendList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_Init(uint Flags)
    {
        Log.Information("[{Function}] {Flags}", nameof(UPLAY_FRIENDS_Init), Flags);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_InviteToGame", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_InviteToGame(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_FRIENDS_GetFriendList), Marshal.PtrToStringAnsi(AccountIdUtf8), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_IsBlackListed", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_IsBlackListed(IntPtr AccountIdUtf8)
    {
        Log.Information("[{Function}] {AccountIdUtf8}", nameof(UPLAY_FRIENDS_IsBlackListed), Marshal.PtrToStringAnsi(AccountIdUtf8));
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_IsFriend", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_IsFriend(IntPtr AccountIdUtf8)
    {
        Log.Information("[{Function}] {AccountIdUtf8}", nameof(UPLAY_FRIENDS_IsFriend), Marshal.PtrToStringAnsi(AccountIdUtf8));
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_RemoveFriendship", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_RemoveFriendship(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_FRIENDS_RemoveFriendship), Marshal.PtrToStringAnsi(AccountIdUtf8), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_RemoveFromBlackList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_RemoveFromBlackList(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_FRIENDS_RemoveFromBlackList), Marshal.PtrToStringAnsi(AccountIdUtf8), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_RequestFriendship", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_RequestFriendship(IntPtr SearchStringUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {SearchStringUtf8} {Overlapped}", nameof(UPLAY_FRIENDS_RequestFriendship), Marshal.PtrToStringAnsi(SearchStringUtf8), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_RespondToGameInvite", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_RespondToGameInvite(uint InvitationId, IntPtr Accept)
    {
        Log.Information("[{Function}] {InvitationId} {Accept}", nameof(UPLAY_FRIENDS_RespondToGameInvite), InvitationId, Accept);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_ShowFriendSelectionUI", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_ShowFriendSelectionUI(IntPtr AccountIdFilterListUtf8, uint AccountIdFilterListLength, IntPtr Overlapped, IntPtr OutResult)
    {
        Log.Information("[{Function}] {AccountIdFilterListUtf8} {AccountIdFilterListLength} {Overlapped} {OutResult}", nameof(UPLAY_FRIENDS_ShowFriendSelectionUI), AccountIdFilterListUtf8, AccountIdFilterListLength, Overlapped, OutResult);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_FRIENDS_ShowInviteFriendsToGameUI", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_FRIENDS_ShowInviteFriendsToGameUI(IntPtr AccountIdFilterListUtf8, uint AccountIdFilterListLength)
    {
        Log.Information("[{Function}] {AccountIdFilterListUtf8} {AccountIdFilterListLength}", nameof(UPLAY_FRIENDS_ShowFriendSelectionUI), AccountIdFilterListUtf8, AccountIdFilterListLength);
        return false;
    }
}
