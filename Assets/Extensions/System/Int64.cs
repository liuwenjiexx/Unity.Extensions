using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static bool IsFlags(this long value, long flags)
        {
            return (value & flags) == flags;
        }

        public static bool HasFlags(this long value, long flags)
        {
            return (value & flags) != 0;
        }
        public static bool CheckFlags(this long value, long includeFlags, long excludeFlags)
        {
            return ((value & excludeFlags) == 0) && ((value & includeFlags) == includeFlags);
        }

    }
}
