using System.Collections;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static bool ItemsEquals(this IEnumerator source, IEnumerator compare)
        {
            while (source.MoveNext())
            {
                if (!compare.MoveNext())
                    return false;

                if (!object.Equals(source.Current, compare.Current))
                    return false;
            }
            return !compare.MoveNext();
        }
    }
}
