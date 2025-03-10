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
    [Guid(IID.IWICFormatConverter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICFormatConverter : IWICBitmapSource
    {
        #region Members inherited from `IWICBitmapSource`

        new void GetSize(
            [Out] out int puiWidth,
            [Out] out int puiHeight);

        new Guid GetPixelFormat();

        new void GetResolution(
            [Out] out double pDpiX,
            [Out] out double pDpiY);

        new void CopyPalette(
            [In] IWICPalette pIPalette);

        new void CopyPixels(
            [In] nint prc, // WICRect*
            [In] int cbStride,
            [In] int cbBufferSize,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] pbBuffer);

        #endregion

        void Initialize(
            [In] IWICBitmapSource pISource,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid dstFormat,
            [In] WICBitmapDitherType dither,
            [In] IWICPalette pIPalette,
            [In] double alphaThresholdPercent,
            [In] WICBitmapPaletteType paletteTranslate);

        bool CanConvert(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid srcPixelFormat,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid dstPixelFormat);
    }
}
