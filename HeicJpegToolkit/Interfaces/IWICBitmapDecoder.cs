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
    [Guid(IID.IWICBitmapDecoder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICBitmapDecoder
    {
        WICBitmapDecoderCapabilities QueryCapability(
            [In] IStream pIStream);

        void Initialize(
            [In] IStream pIStream,
            [In] WICDecodeOptions cacheOptions);

        Guid GetContainerFormat();

        IWICBitmapDecoderInfo GetDecoderInfo();

        void CopyPalette(
            [In] IWICPalette pIPalette);

        IWICMetadataQueryReader GetMetadataQueryReader();

        IWICBitmapSource GetPreview();

        void GetColorContexts(
            [In] int cCount,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[]? ppIColorContexts,
            [Out] out int pcActualCount);

        IWICBitmapSource GetThumbnail();

        int GetFrameCount();

        IWICBitmapFrameDecode GetFrame(
            [In] int index);
    }
}
