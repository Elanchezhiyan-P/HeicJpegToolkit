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
    [Guid(IID.IWICBitmapEncoder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICBitmapEncoder
    {
        void Initialize(
            [In] IStream pIStream,
            [In] WICBitmapEncoderCacheOption cacheOption);

        Guid GetContainerFormat();

        IWICBitmapEncoderInfo GetEncoderInfo();

        void SetColorContexts(
            [In] int cCount,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[] ppIColorContext);

        void SetPalette(
            [In] IWICPalette pIPalette);

        void SetThumbnail(
            [In] IWICBitmapSource pIThumbnail);

        void SetPreview(
            [In] IWICBitmapSource pIPreview);

        void CreateNewFrame(
            [Out] out IWICBitmapFrameEncode ppIFrameEncode,
            [In, Out] ref IPropertyBag2? ppIEncoderOptions);

        void Commit();

        IWICMetadataQueryWriter GetMetadataQueryWriter();
    }
}
