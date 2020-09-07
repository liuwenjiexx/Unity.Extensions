using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO.Extensions
{
    public static partial class Extension
    {
        public static void FileClearAttributes(this string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                File.SetAttributes(path, FileAttributes.Normal);
        }


        public static bool FileEqualContent(this string file1, string file2)
        {
            FileInfo fileInfo1 = new FileInfo(file1);
            FileInfo fileInfo2 = new FileInfo(file2);
            bool equal = true;
            if (fileInfo1.Exists && fileInfo2.Exists && fileInfo1.Length == fileInfo2.Length)
            {
                using (var fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
                using (var fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
                {
                    int buffSize = 1024 * 4;

                    buffSize = Math.Min(buffSize, (int)fileInfo1.Length);
                    buffSize = Math.Min(buffSize, (int)fileInfo2.Length);

                    byte[] buff1 = new byte[buffSize];
                    byte[] buff2 = new byte[buffSize];
                    int count1;
                    int count2;
                    while (equal)
                    {
                        count1 = fs1.Read(buff1, 0, buff1.Length);
                        if (count1 == 0)
                            break;
                        count2 = fs2.Read(buff2, 0, buff2.Length);
                        if (count1 != count2)
                        {
                            equal = false;
                            break;
                        }
                        for (int i = 0; i < count1; i++)
                        {
                            if (buff1[i] != buff2[i])
                            {
                                equal = false;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                equal = false;
            }
            return equal;
        }

        public static bool FileCopyIfChanged(this string srcFile, string dstFile)
        {
            bool changed = false;

            if (!FileEqualContent(srcFile, dstFile))
            {
                changed = true;
            }
            if (changed)
            {
                string dstDir = Path.GetDirectoryName(dstFile);
                if (!string.IsNullOrEmpty(dstDir) && !Directory.Exists(dstDir))
                    Directory.CreateDirectory(dstDir);
                if (File.Exists(dstFile))
                    File.SetAttributes(dstFile, FileAttributes.Normal);
                File.Copy(srcFile, dstFile, true);
            }
            return changed;
        }

    }
}
