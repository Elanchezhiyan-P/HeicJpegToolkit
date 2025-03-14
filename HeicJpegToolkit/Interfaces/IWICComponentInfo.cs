﻿using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICComponentInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICComponentInfo
    {
        WICComponentType GetComponentType();

        Guid GetCLSID();

        WICComponentSigning GetSigningStatus();

        void GetAuthor(
            [In] int cchAuthor,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzAuthor,
            [Out] out int pcchActual);

        Guid GetVendorGUID();

        void GetVersion(
            [In] int cchVersion,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzVersion,
            [Out] out int pcchActual);

        void GetSpecVersion(
            [In] int cchSpecVersion,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzSpecVersion,
            [Out] out int pcchActual);

        void GetFriendlyName(
            [In] int cchFriendlyName,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzFriendlyName,
            [Out] out int pcchActual);
    }
}
