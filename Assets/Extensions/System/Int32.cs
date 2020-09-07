using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Extensions
{
    public static partial class Extension
    {
        /// <summary>
        /// (value & flags) == flags
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool IsFlags(this int value, int flags)
        {
            return (value & flags) == flags;
        }
        /// <summary>
        /// (value & flags) != 0
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasFlags(this int value, int flags)
        {
            return (value & flags) != 0;
        }

        public static bool CheckFlags(this int value, int includeFlags, int excludeFlags)
        {
            return ((value & excludeFlags) == 0) && ((value & includeFlags) == includeFlags);
        }

    }
}
