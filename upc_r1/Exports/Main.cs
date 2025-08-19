using DllShared;
using Shared;

namespace upc_r1.Exports;

public static class Main
{
    public static uint ProductId = 0;

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_GetLastError", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_GetLastError(IntPtr OutErrorString)
    {
        Log.Verbose("[{Function}] {OutErrorString}", nameof(UPLAY_GetLastError), OutErrorString);
        Marshal.WriteIntPtr(OutErrorString, Marshal.StringToHGlobalAnsi(string.Empty));
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_HasOverlappedOperationCompleted", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_HasOverlappedOperationCompleted(IntPtr Overlapped)
    {
        if (Overlapped == IntPtr.Zero)
            return false;
        var lapped = Marshal.PtrToStructure<UPLAY_Overlapped>(Overlapped);
        return lapped.Completed;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_GetOverlappedOperationResult", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_GetOverlappedOperationResult(IntPtr Overlapped, IntPtr OutResult)
    {
        var lapped = Marshal.PtrToStructure<UPLAY_Overlapped>(Overlapped);
        Marshal.WriteInt32(OutResult, (int)lapped.Result);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PeekNextEvent", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PeekNextEvent(IntPtr OutEvent)
    {
        Log.Verbose("[{Function}] {OutEvent}", nameof(UPLAY_PeekNextEvent), OutEvent);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_GetNextEvent", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_GetNextEvent(IntPtr OutEvent)
    {
        Log.Verbose("[{Function}] {OutEvent}", nameof(UPLAY_GetNextEvent), OutEvent);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_Init", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_Init()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_Init));
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_Start", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPLAY_Start(uint UplayId, uint Flags)
    {
        if (UPC_Json.Instance.UseDebug)
        {
            MainLogger.LevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
            MainLogger.FileLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
        }
        MainLogger.CreateNew();
        Log.Information("[{Function}] {UplayId} {Flags}", nameof(UPLAY_Start), UplayId, Flags);

        ProductId = UplayId;
        LoadDll.PluginPath = "r1";
        LoadDll.LoadPlugins();
        return (int)UplayStartResult.Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_Startup", CallConvs = [typeof(CallConvCdecl)])]
    public static int UPLAY_Startup(uint UplayId, uint GameVersion, IntPtr LanguageCountryCodeUtf8)
    {
        if (UPC_Json.Instance.UseDebug)
        {
            MainLogger.LevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
            MainLogger.FileLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;
        }
        MainLogger.CreateNew();
        Log.Verbose("[{Function}] {UplayId} {GameVersion} {LanguageCountryCodeUtf8}", nameof(UPLAY_Startup), UplayId, GameVersion, LanguageCountryCodeUtf8);
        ProductId = UplayId;
        LoadDll.PluginPath = "r1";
        LoadDll.LoadPlugins();
        return (int)UplayStartResult.Ok;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_Update", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_Update()
    {
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_Quit", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_Quit()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_Quit));
        LoadDll.FreePlugins();
        MainLogger.Close();
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SetLanguage", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SetLanguage(IntPtr LanguageCountryCode)
    {
        Log.Verbose("[{Function}] {LanguageCountryCode}", nameof(UPLAY_SetLanguage), LanguageCountryCode);
        string? langCode = Marshal.PtrToStringUTF8(LanguageCountryCode);
        if (!string.IsNullOrEmpty(langCode))
            UPC_Json.Instance.Account.Country = langCode;
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_SetGameSession", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_SetGameSession(IntPtr GameSessionIdentifier, IntPtr SessionData, uint Flags)
    {
        Log.Verbose("[{Function}] {GameSessionIdentifier} {SessionData} {Flags}", nameof(UPLAY_SetLanguage), GameSessionIdentifier, SessionData, Flags);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ClearGameSession", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ClearGameSession()
    {
        Log.Verbose("[{Function}]", nameof(UPLAY_ClearGameSession));
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_PRESENCE_SetPresence", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_PRESENCE_SetPresence(uint presenceId, IntPtr tokens)
    {
        Log.Verbose("[{Function}] {presenceId} {tokens}", nameof(UPLAY_PRESENCE_SetPresence), presenceId, tokens);
        return true;
    }
}
