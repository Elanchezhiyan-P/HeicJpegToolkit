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
    [Guid(IID.IWICMetadataBlockWriter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWICMetadataBlockWriter : IWICMetadataBlockReader
    {
        #region Members inherited from `IWICMetadataBlockReader`

        new Guid GetContainerFormat();

        new int GetCount();

        new IWICMetadataReader GetReaderByIndex(
            [In] int nIndex);

        new IEnumUnknown GetEnumerator();

        #endregion

        void InitializeFromBlockReader(
            [In] IWICMetadataBlockReader pIMDBlockReader);

        IWICMetadataWriter GetWriterByIndex(
            [In] int nIndex);

        void AddWriter(
            [In] IWICMetadataWriter pIMetadataWriter);

        void SetWriterByIndex(
            [In] int nIndex,
            [In] IWICMetadataWriter pIMetadataWriter);

        void RemoveWriterByIndex(
            [In] int nIndex);
    }
}
