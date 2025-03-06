using HeicJpegToolkit.Helpers.Constants;
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
    [Guid(IID.IWICPalette)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICPalette
    {
        void InitializePredefined(
            [In] WICBitmapPaletteType ePaletteType,
            [In] bool fAddTransparentColor);

        void InitializeCustom(
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 1)] int[] pColors,
            [In] int cCount);

        void InitializeFromBitmap(
            [In] IWICBitmapSource pISurface,
            [In] int cCount,
            [In] bool fAddTransparentColor);

        void InitializeFromPalette(
            [In] IWICPalette pIPalette);

        WICBitmapPaletteType GetType();

        int GetColorCount();

        void GetColors(
            [In] int cCount,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeParamIndex = 0)] int[]? pColors,
            [Out] out int pcActualColors);

        bool IsBlackWhite();

        bool IsGrayscale();

        bool HasAlpha();
    }
}
