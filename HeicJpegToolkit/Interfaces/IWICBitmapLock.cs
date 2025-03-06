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
