using System.Collections;
using System.Collections.Generic;

namespace System.Extensions
{
    public static partial class Extension
    {
        public static TValue GetOrCreateValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TKey, TValue> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            TValue value;
            if (!self.TryGetValue(key, out value))
            {
                value = factory(key);
                self[key] = value;
            }
            return value;
        }

        public static void Extend(this IDictionary target, params IDictionary[] objs)
        {
            foreach (var obj in objs)
            {
                foreach (var key in obj.Keys)
                {
                    target[key] = obj[key];
                }
            }

        }
    }

}
