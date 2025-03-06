using HeicJpegToolkit.Helpers.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICBitmapSource)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICBitmapSource
    {
        void GetSize(
            [Out] out int puiWidth,
            [Out] out int puiHeight);

        Guid GetPixelFormat();

        void GetResolution(
            [Out] out double pDpiX,
            [Out] out double pDpiY);

        void CopyPalette(
            [In] IWICPalette pIPalette);

        void CopyPixels(
            [In] nint prc, // WICRect*
            [In] int cbStride,
            [In] int cbBufferSize,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] pbBuffer);
    }
}
