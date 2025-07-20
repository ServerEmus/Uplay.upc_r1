using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace upc_r1;

public static class Basics
{
    public static void WriteOverlappedResult(IntPtr Overlapped, bool IsSuccess, UPLAY_OverlappedResult result)
    {
        var overlapped = Marshal.PtrToStructure<UPLAY_Overlapped>(Overlapped);
        overlapped.Completed = IsSuccess;
        overlapped.Result = result;
        Marshal.StructureToPtr(overlapped, Overlapped, false);
    }

    public static IntPtr GetListPtr<T>(List<T> values) where T : struct
    {
        IntPtr main_ptr = Marshal.AllocHGlobal(Marshal.SizeOf<IntPtr>() * values.Count);
        int indx = 0;
        foreach (var item in values)
        {
            IntPtr iptr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
            Marshal.StructureToPtr(item, iptr, false);
            Marshal.WriteIntPtr(main_ptr, indx * Marshal.SizeOf<IntPtr>(), iptr);
            indx++;
        }
        return main_ptr;
    }

    public static List<T> GetListFromPtr<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors)] T>(int count, IntPtr listPointer) where T : struct
    {
        List<T> list = [];
        for (int i = 0; i < count; i++)
        {
            var ptr = Marshal.ReadIntPtr(listPointer, i * Marshal.SizeOf<IntPtr>());
            T structure = Marshal.PtrToStructure<T>(ptr);
            list.Add(structure);
            Marshal.FreeHGlobal(ptr);
        }
        Marshal.FreeHGlobal(listPointer);
        return list;
    }
}