﻿using HeicJpegToolkit.Helpers.Constants;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IEnumString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumString
    {
        void Next(
            [In] int celt,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] rgelt,
            [Out] out int pceltFetched);

        void Skip(
            [In] int celt);

        void Reset();

        IEnumString Clone();
    }
}
