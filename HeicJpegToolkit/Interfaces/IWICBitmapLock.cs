using HeicJpegToolkit.Helpers.Constants;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICBitmapLock)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICBitmapLock
    {
        void GetSize(
            [Out] out int puiWidth,
            [Out] out int puiHeight);

        int GetStride();

        nint GetDataPointer( // byte*
            [Out] out int pcbBufferSize);

        Guid GetPixelFormat();
    }
}
