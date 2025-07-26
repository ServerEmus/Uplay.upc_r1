namespace upc_r1.Exports;

/// <summary>
/// Chat Export has been abandoned by Ubisoft. Original function does NOTHING
/// </summary>
internal class Party
{

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_DisablePartyMemberMenuItem", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_DisablePartyMemberMenuItem(uint Id)
    {
        Log.Information("[{Function}] {Id}", nameof(UPLAY_PARTY_DisablePartyMemberMenuItem), Id);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_EnablePartyMemberMenuItem", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_EnablePartyMemberMenuItem(uint Id, uint MenuItemMode, IntPtr Filter)
    {
        Log.Information("[{Function}] {Id} {MenuItemMode} {Filter}", nameof(UPLAY_PARTY_EnablePartyMemberMenuItem), Id, MenuItemMode, Filter);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_GetFullMemberList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_GetFullMemberList(IntPtr OutMemberList)
    {
        Log.Information("[{Function}] {OutMemberList}", nameof(UPLAY_PARTY_GetFullMemberList), OutMemberList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_GetId", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPLAY_PARTY_GetId()
    {
        Log.Information("[{Function}]", nameof(UPLAY_PARTY_GetId));
        return 0;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_GetInGameMemberList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_GetInGameMemberList(IntPtr OutMemberList)
    {
        Log.Information("[{Function}] {OutMemberList}", nameof(UPLAY_PARTY_GetInGameMemberList), OutMemberList);
        // Deprecated.
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_Init(uint Flags)
    {
        Log.Information("[{Function}] {Flags}", nameof(UPLAY_PARTY_Init), Flags);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_InvitePartyToGame", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_InvitePartyToGame(IntPtr Overlapped)
    {
        Log.Information("[{Function}] {Overlapped}", nameof(UPLAY_PARTY_InvitePartyToGame), Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_InviteToParty", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_InviteToParty(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_PARTY_InvitePartyToGame), AccountIdUtf8, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_IsInParty", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_IsInParty(IntPtr AccountIdUtf8)
    {
        Log.Information("[{Function}] {AccountIdUtf8}", nameof(UPLAY_PARTY_IsInParty), AccountIdUtf8);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_IsPartyLeader", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_IsPartyLeader(IntPtr AccountIdUtf8)
    {
        Log.Information("[{Function}] {AccountIdUtf8}", nameof(UPLAY_PARTY_IsPartyLeader), AccountIdUtf8);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_PromoteToLeader", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_PromoteToLeader(IntPtr AccountIdUtf8, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {AccountIdUtf8} {Overlapped}", nameof(UPLAY_PARTY_InvitePartyToGame), AccountIdUtf8, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_RespondToGameInvite", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_RespondToGameInvite(uint InvitationId, bool Accept)
    {
        Log.Information("[{Function}] {InvitationId} {Accept}", nameof(UPLAY_PARTY_RespondToGameInvite), InvitationId, Accept);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_SetGuest", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_SetGuest(IntPtr guestId, IntPtr Overlapped)
    {
        Log.Information("[{Function}] {guestId} {Overlapped}", nameof(UPLAY_PARTY_SetGuest), guestId, Overlapped);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_SetUserData", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_SetUserData(IntPtr DataBlob)
    {
        Log.Information("[{Function}] {DataBlob}", nameof(UPLAY_PARTY_SetUserData), DataBlob);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PARTY_ShowGameInviteOverlayUI", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PARTY_ShowGameInviteOverlayUI(uint InvitationId)
    {
        Log.Information("[{Function}] {InvitationId}", nameof(UPLAY_PARTY_ShowGameInviteOverlayUI), InvitationId);
        return false;
    }
}
