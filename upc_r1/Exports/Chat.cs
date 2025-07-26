namespace upc_r1.Exports;

/// <summary>
/// Chat Export has been abandoned by Ubisoft. Original function does NOTHING
/// </summary>
internal class Chat
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_CHAT_GetHistory", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_CHAT_GetHistory(IntPtr accountIdUtf8, uint maxNumberOfMessages, IntPtr OutHistoryList, IntPtr Overlapped)
    {
        Log.Verbose("[{Function}] {Param1} {Param2} {Param3} {Param4}", nameof(UPLAY_CHAT_GetHistory), accountIdUtf8, maxNumberOfMessages, OutHistoryList, Overlapped);
        Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Failed);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_CHAT_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_CHAT_Init(int aFlags)
    {
        Log.Verbose("[{Function}] {Param1}", nameof(UPLAY_CHAT_Init), aFlags);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_CHAT_ReleaseHistoryList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_CHAT_ReleaseHistoryList(IntPtr HistoryList)
    {
        Log.Verbose("[{Function}] {Param1}", nameof(UPLAY_CHAT_ReleaseHistoryList), HistoryList);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_CHAT_SendMessage", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_CHAT_SendMessage(IntPtr AccountIdUtf8, IntPtr MessageUtf8, IntPtr Data)
    {
        Log.Verbose("[{Function}] {Param1} {Param2} {Param3}", nameof(UPLAY_CHAT_SendMessage), AccountIdUtf8, MessageUtf8, Data);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_CHAT_SetMessagesRead", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_CHAT_SetMessagesRead(IntPtr AccountIdUtf8)
    {
        Log.Verbose("[{Function}] {Param1}", nameof(UPLAY_CHAT_SetMessagesRead), AccountIdUtf8);
        return false;
    }
}
