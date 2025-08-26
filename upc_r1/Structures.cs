namespace upc_r1;

[StructLayout(LayoutKind.Sequential)]
public struct UPLAY_Overlapped
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public ulong[] Internal = new ulong[2];

    public readonly bool Completed
    {
        get => Internal[0] == 1;
        set => Internal[0] = Convert.ToUInt64(value);
    }

    public readonly UPLAY_OverlappedResult Result
    {
        get => (UPLAY_OverlappedResult)Convert.ToInt32(Internal[1]);
        set => Internal[1] = Convert.ToUInt64(value);
    }

    public UPLAY_Overlapped()
    {

    }

}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_DataBlob
{
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr data;
    [MarshalAs(UnmanagedType.U4)]
    public uint numBytes;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_Event
{
    [MarshalAs(UnmanagedType.I4)]
    public UPLAY_EventType type;
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr @event;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_SAVE_Game
{
    [MarshalAs(UnmanagedType.U8)]
    public ulong id;
    [MarshalAs(UnmanagedType.LPStr)]
    public string nameUtf8;
    [MarshalAs(UnmanagedType.U8)]
    public ulong size;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_USER_CdKey
{
    [MarshalAs(UnmanagedType.U4)]
    public uint uplayId;
    [MarshalAs(UnmanagedType.LPStr)]
    public string keyUtf8;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_ACH_Achievement
{
    [MarshalAs(UnmanagedType.U4)]
    public uint id;
    [MarshalAs(UnmanagedType.LPStr)]
    public string nameUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string descriptionUtf8;
    [MarshalAs(UnmanagedType.U1)]
    public bool earned;
}


[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_USER_GameSession
{
    public ulong id;
    public UPLAY_DataBlob Data;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_PRESENCE_Presence
{
    public UPLAY_PRESENCE_Status status;
    [MarshalAs(UnmanagedType.LPStr)]
    public string richPresenceUtf8;
    public uint state;
    public IntPtr GameSessionPtr;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_FRIEND_Friend
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string accountIdUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string nickUtf8;
    [MarshalAs(UnmanagedType.U4)]
    public Uplay.Uplaydll.Relationship relationship;
    [MarshalAs(UnmanagedType.U4)]
    public uint avatarId;
    public IntPtr PresencePtr;
    public bool isBlacklisted;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_PARTY_Member
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string accountIdUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string nickUtf8;
    [MarshalAs(UnmanagedType.U4)]
    public uint avatarId;
    public UPLAY_DataBlob data;
    public UPLAY_PARTY_MemberFlag flag;
    public IntPtr presencePtr;
    public IntPtr partyHostIfGuest;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_WIN_Reward
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string idUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string nameUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string descriptionUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string urlUtf8;
    public uint Units;
    [MarshalAs(UnmanagedType.LPStr)]
    public string gameCodeUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string platformCodeUtf8;
    [MarshalAs(UnmanagedType.LPStr)]
    public string imgUrlUtf8;
    public bool redeemed;
}

[StructLayout(LayoutKind.Sequential)]
public struct UplayKey
{
    [MarshalAs(UnmanagedType.SysInt)]
    public IntPtr CdKey; // const char*
}

// Events

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_FRIENDS_FriendUpdated
{
    public IntPtr previousFriendPtr;
    public IntPtr updatedFriendPtr;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_FRIENDS_GameInviteAccepted
{
    public IntPtr gameSessionPtr;
    [MarshalAs(UnmanagedType.LPStr)]
    public string accountIdUtf8;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_PARTY_UserDataChanged
{
    public IntPtr partyMemberPtr;
}


[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_PARTY_GameInviteReceived
{
    public ulong invitationid;
    public IntPtr fromPartyMemberPtr;
    public IntPtr gameSessionPtr;
}

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct UPLAY_PARTY_GameInviteAccepted
{
    public ulong invitationid;
    public IntPtr gameSessionPtr;
}