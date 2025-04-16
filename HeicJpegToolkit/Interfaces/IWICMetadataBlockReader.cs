using HeicJpegToolkit.Helpers.Constants;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IWICMetadataBlockReader)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICMetadataBlockReader
    {
        Guid GetContainerFormat();

        int GetCount();

        IWICMetadataReader GetReaderByIndex(
            [In] int nIndex);

        IEnumUnknown GetEnumerator();
    }
}
