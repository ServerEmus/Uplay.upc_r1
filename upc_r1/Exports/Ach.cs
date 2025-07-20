namespace upc_r1.Exports;

internal static class Ach
{
    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_EarnAchievement", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_EarnAchievement(uint AchivementId, IntPtr Overlapped)
    {
        Log.Information(nameof(UPLAY_ACH_EarnAchievement), [AchivementId, Overlapped]);
        var achi = UPC_Json.Instance.Achis.FirstOrDefault(x=>x.Id == AchivementId);
        if (achi == null)
        {
            Basics.WriteOverlappedResult(Overlapped, false, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Failed);
            return false;
        }
        achi.Achieved = true;
        UPC_Json.SaveToJson();
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_GetAchievementImage", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_GetAchievementImage(uint aId, IntPtr aOutImage, IntPtr aOverlapped)
    {
        Log.Information(nameof(UPLAY_ACH_GetAchievementImage), [aId, aOutImage, aOverlapped]);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_GetAchievements", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_GetAchievements(uint Filter, IntPtr AccountIdUtf8OrNULLIfCurrentUser, IntPtr OutAchievementList, IntPtr Overlapped)
    {
        Log.Information(nameof(UPLAY_ACH_GetAchievements), [Filter, AccountIdUtf8OrNULLIfCurrentUser, OutAchievementList, Overlapped]);
        List<UPLAY_ACH_Achievement> Achis = [];
        foreach (var achi in UPC_Json.Instance.Achis)
        {
            Achis.Add(new()
            { 
                id = achi.Id,
                nameUtf8 = achi.Name,
                descriptionUtf8 = achi.Description,
                earned = achi.Achieved,
            });
        }
        WriteOutList(OutAchievementList, Achis);
        Basics.WriteOverlappedResult(Overlapped, true, UPLAY_OverlappedResult.UPLAY_OverlappedResult_Ok);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_ReleaseAchievementImage", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_ReleaseAchievementImage(IntPtr Image)
    {
        Log.Information(nameof(UPLAY_ACH_ReleaseAchievementImage), [Image]);
        return false;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_ReleaseAchievementList", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_ReleaseAchievementList(IntPtr List)
    {
        Log.Information(nameof(UPLAY_ACH_ReleaseAchievementList), [List]);
        FreeList(List);
        return true;
    }

    [UnmanagedCallersOnly(EntryPoint = "UPLAY_ACH_Write", CallConvs = [typeof(CallConvCdecl)])]
    public static bool UPLAY_ACH_Write(IntPtr aAchievement)
    {
        Log.Information(nameof(UPLAY_ACH_Write), [aAchievement]);
        return false;
    }
}
