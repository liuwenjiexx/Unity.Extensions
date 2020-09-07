using System.Collections;
using System.Collections.Generic;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> match)
        {
            int index = -1;
            int i = 0;
            foreach (var it in source)
            {
                if (match(it))
                {
                    index = i;
                    break;
                }
                i++;
            }

            return index;
        }

        public static T[] ToArrayOf<T>(this IEnumerable source)
        {
            List<T> result = new List<T>();
            foreach (var o in source)
                result.Add((T)o);
            return result.ToArray();
        }



        public static bool ItemsEquals(this IEnumerable source, IEnumerable compare)
        {
            if (source == null)
            {
                if ((compare != null) && compare.GetEnumerator().MoveNext())
                {
                    return false;
                }
                return true;
            }
            if (compare == null)
            {
                if ((source != null) && source.GetEnumerator().MoveNext())
                {
                    return false;
                }
                return true;
            }

            return ItemsEquals(source.GetEnumerator(), compare.GetEnumerator());
        }

        public static bool ItemsEquals<T>(this T[] source, T[] compare)
        {
            if (source == null)
            {
                if ((compare != null) && (compare.Length != 0))
                {
                    return false;
                }
                return true;
            }
            if (compare == null)
            {
                if ((source != null) && (source.Length != 0))
                {
                    return false;
                }
                return true;
            }
            int len = source.Length;
            if (len != compare.Length)
                return false;

            if (len != 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (!object.Equals(source[i], compare[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static void CopyTo<T>(this IEnumerable<T> source, IList<T> list)
        {
            foreach (var item in source)
            {
                list.Add(item);
            }
        }

        public static void CopyTo<T>(this IEnumerable<T> source, IList<T> list, int index, int count)
        {
            int n = 0;
            foreach (var item in source)
            {
                if (n >= count)
                    break;
                list.Insert(index + n, item);
                n++;
            }
        }

    }
}
