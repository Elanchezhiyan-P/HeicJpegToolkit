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
    [Guid(IID.IWICColorContext)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICColorContext
    {
        void InitializeFromFilename(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wzFilename);

        void InitializeFromMemory(
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pbBuffer,
            [In] int cbBufferSize);

        void InitializeFromExifColorSpace(
            [In] ExifColorSpace value);

        WICColorContextType GetType();

        void GetProfileBytes(
            [In] int cbBuffer,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 0)] byte[]? pbBuffer,
            [Out] out int pcbActual);

        ExifColorSpace GetExifColorSpace();
    }
}
