using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    [Flags]
    public enum WICMetadataCreationOptions : int
    {
        WICMetadataCreationDefault = 0x00000000,
        WICMetadataCreationAllowUnknown = WICMetadataCreationDefault,
        WICMetadataCreationFailUnknown = 0x00010000,
        WICMetadataCreationMask = unchecked((int)0xFFFF0000),
    }
}
