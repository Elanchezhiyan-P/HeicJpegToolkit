using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    [Flags]
    public enum StreamAccessMode : int
    {
        GENERIC_WRITE = 0x40000000,
        GENERIC_READ = unchecked((int)0x80000000U),
    }
}
