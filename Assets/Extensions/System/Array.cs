using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Extensions
{
    public static partial class Extension
    {
        static Random random = new Random();

        public static bool HasElement(this ICollection source)
        {
            return source != null && source.Count > 0;
        }

        public static int IndexOf<T>(this T[] source, T value)
        {

            for (int i = 0, len = source.Length; i < len; i++)
                if (object.Equals(source[i], value))
                    return i;

            return -1;
        }

        public static int IndexOf<T>(this T[] source, Func<T, bool> equals)
        {
            for (int i = 0, len = source.Length; i < len; i++)
                if (equals(source[i]))
                    return i;

            return -1;
        }

        public static int LastIndexOf<T>(this T[] source, Func<T, bool> match)
        {
            int index = -1;
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (match(source[i]))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public static void Swap<T>(this T[] source, int index1, int index2)
        {
            T tmp = source[index1];
            source[index1] = source[index2];
            source[index2] = tmp;
        }

        public static T[] RemoveNull<T>(this T[] source)
          where T : class
        {

            if (source == null || source.Length <= 0)
                return new T[0];

            List<T> result = new List<T>(source.Length);
            foreach (var item in source)
            {
                if (item != null)
                    result.Add(item);
            }
            return result.ToArray();
        }


        public static string[] RemoveNullOrEmpty(this string[] source)
        {

            if (source == null)
                return new string[0];

            List<string> result = new List<string>(source.Length);
            foreach (var item in source)
            {
                if (!string.IsNullOrEmpty(item))
                    result.Add(item);
            }
            return result.ToArray();
        }

        public static T[] InsertArrayElementAtIndex<T>(this T[] array, int index, T item)
        {
            List<T> list;
            if (array != null)
                list = new List<T>(array);
            else
                list = new List<T>();
            list.Insert(index, item);
            return list.ToArray();
        }

        public static T[] DeleteArrayElementAtIndex<T>(this T[] array, int index)
        {
            List<T> list;
            if (array != null)
                list = new List<T>(array);
            else
                list = new List<T>();
            list.RemoveAt(index);
            return list.ToArray();
        }

        public static int RandomIndexWithWeight<T>(this IEnumerable<T> items, Func<T, float> getWeight)
        {
            if (items == null)
                return -1;

            float totalWeight = items.Sum(o => getWeight(o));
            int index = -1;
            if (totalWeight > 0)
            {
                float value = (float)random.NextDouble();
                float weight = 0f;
                int _i = 0;
                foreach (var item in items)
                {
                    float n = getWeight(item) / totalWeight;
                    weight += n;
                    if (value < weight)
                    {
                        index = _i;
                        break;
                    }
                    _i++;
                }
            }
            return index;
        }

    }
}
