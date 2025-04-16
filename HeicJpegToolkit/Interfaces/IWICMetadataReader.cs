using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Structures;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICMetadataReader)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICMetadataReader
    {
        Guid GetMetadataFormat();

        IWICMetadataHandlerInfo GetMetadataHandlerInfo();

        int GetCount();

        void GetValueByIndex(
            [In] int nIndex,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pvarSchema,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pvarId,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pvarValue);

        void GetValue(
            [In, MarshalAs(UnmanagedType.Struct)] PROPVARIANT pvarSchema,
            [In, MarshalAs(UnmanagedType.Struct)] PROPVARIANT pvarId,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref PROPVARIANT pvarValue);

        IWICEnumMetadataItem GetEnumerator();
    }
}
