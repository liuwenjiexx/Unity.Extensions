using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.IO
{
    public static partial class Extensions
    {
        public static void ClearFileAttributes(this string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                File.SetAttributes(path, FileAttributes.Normal);
        }


        public static bool PathStartsWithDirectory(this string path, string dir)
        {
            char separatorChar = Path.DirectorySeparatorChar;
            if (separatorChar == '/')
            {
                path = path.Replace('\\', separatorChar);
                dir = dir.Replace('\\', separatorChar);
            }
            else
            {
                path = path.Replace('/', separatorChar);
                dir = dir.Replace('/', separatorChar);
            }
            path = path.ToLower();
            dir = dir.ToLower();
            if (!dir.EndsWith(separatorChar.ToString()))
                dir += separatorChar;
            return path.StartsWith(dir);
        }

        public static bool PathStartsWith(this string path, string dir)
        {
            char separatorChar = Path.DirectorySeparatorChar;
            if (separatorChar == '/')
            {
                path = path.Replace('\\', separatorChar);
                dir = dir.Replace('\\', separatorChar);
            }
            else
            {
                path = path.Replace('/', separatorChar);
                dir = dir.Replace('/', separatorChar);
            }
            path = path.ToLower();
            dir = dir.ToLower();

            return path.StartsWith(dir);
        }
    }

}
