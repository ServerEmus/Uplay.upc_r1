namespace upc_r1;

public enum UPLAY_EventType
{
    UPLAY_Event_FriendsFriendListUpdated = 10000,
    UPLAY_Event_FriendsFriendUpdated,
    UPLAY_Event_FriendsGameInviteAccepted,
    UPLAY_Event_FriendsMenuItemSelected,
    UPLAY_Event_PartyMemberListChanged = 20000,
    UPLAY_Event_PartyMemberUserDataUpdated,
    UPLAY_Event_PartyLeaderChanged,
    UPLAY_Event_PartyGameInviteReceived,
    UPLAY_Event_PartyGameInviteAccepted,
    UPLAY_Event_PartyMemberMenuItemSelected,
    UPLAY_Event_PartyMemberUpdated,
    UPLAY_Event_PartyInviteReceived,
    UPLAY_Event_PartyMemberJoined,
    UPLAY_Event_PartyMemberLeft,
    UPLAY_Event_OverlayActivated = 30000,
    UPLAY_Event_OverlayHidden,
    UPLAY_Event_RewardRedeemed = 40000,
    UPLAY_Event_UserAccountSharing = 50000,
    UPLAY_Event_UserConnectionLost,
    UPLAY_Event_UserConnectionRestored
}
public enum UPLAY_OVERLAY_Section
{
    UPLAY_OverlaySection_Show,
    UPLAY_OverlaySection_Home,
    UPLAY_OverlaySection_Achievements,
    UPLAY_OverlaySection_Actions,
    UPLAY_OverlaySection_Chat,
    UPLAY_OverlaySection_Friends,
    UPLAY_OverlaySection_Party,
    UPLAY_OverlaySection_Rewards
}

public enum UPLAY_OverlappedResult
{
    UPLAY_OverlappedResult_Ok,
    UPLAY_OverlappedResult_InvalidArgument,
    UPLAY_OverlappedResult_ConnectionError,
    UPLAY_OverlappedResult_NotFound,
    UPLAY_OverlappedResult_NotAPartyLeader,
    UPLAY_OverlappedResult_PartyFull,
    UPLAY_OverlappedResult_Failed,
    UPLAY_OverlappedResult_AlreadyOpened,
    UPLAY_OverlappedResult_SlotLocked
}

public enum UplayStartResult
{
    Ok,
    Failed,
    ExitProcessRequired,
    InstallationError,
    DesktopInteractionRequired
}

[Flags]
public enum UplayStartFlags
{
    None = 0
}


[Flags]
public enum UPLAY_FRIENDS_FriendListFilter
{
    FriendRequestSent = 1,
    FriendRequestReceived = 2,
    Friends = 4,
    BlackListed = 8,
    All = 0xFF,
}

[Flags]
public enum UPLAY_PARTY_MemberFlag
{
    IsPartyLeader = 1,
    IsLocal = 2,
    HasGuest = 4,
    IsGuest = 8,
}

[Flags]
public enum UPLAY_ACH_Filter
{
    All,
    Taken,
}

[Flags]
public enum UPLAY_USER_GameSessionFlags
{
    NotJoinable = 1,
}

public enum UPLAY_PRESENCE_Status
{
    InGame,
    Online,
    Away,
    Offline,
}