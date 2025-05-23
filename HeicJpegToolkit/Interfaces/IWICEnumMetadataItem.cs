﻿using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Structures;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICEnumMetadataItem)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICEnumMetadataItem
    {
        void Next(
           [In] int celt,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltSchema,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltId,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltValue,
           [Out] out int pceltFetched);

        void Skip(
            [In] int celt);

        void Reset();

        IWICEnumMetadataItem Clone();
    }
}
