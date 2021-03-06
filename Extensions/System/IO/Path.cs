﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.IO.Extensions
{
    public static partial class Extension
    {

        public static string ReplaceDirectorySeparatorChar(this string path)
        {
            char separatorChar = Path.DirectorySeparatorChar;
            if (separatorChar == '/')
                path = path.Replace('\\', separatorChar);
            else
                path = path.Replace('/', separatorChar);

            return path;
        }


        public static bool PathStartsWithDirectory(this string path, string dir)
        {
            char separatorChar = Path.DirectorySeparatorChar;
            path = path.ReplaceDirectorySeparatorChar();
            dir = dir.ReplaceDirectorySeparatorChar();

            path = path.ToLower();
            dir = dir.ToLower();
            if (!dir.EndsWith(separatorChar.ToString()))
                dir += separatorChar;
            return path.StartsWith(dir);
        }

        public static bool PathStartsWith(this string path, string dir)
        {

            path = path.ReplaceDirectorySeparatorChar();
            dir = dir.ReplaceDirectorySeparatorChar();
            path = path.ToLower();
            dir = dir.ToLower();

            return path.StartsWith(dir);
        }
    }

}
