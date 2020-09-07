using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Reflection.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<Assembly> Referenced(this IEnumerable<Assembly> assemblies, Assembly referenced)
        {
            string fullName = referenced.FullName;

            foreach (var ass in assemblies)
            {
                if (referenced == ass)
                {
                    yield return ass;
                }
                else
                {
                    foreach (var refAss in ass.GetReferencedAssemblies())
                    {
                        if (fullName == refAss.FullName)
                        {
                            yield return ass;
                            break;
                        }
                    }
                }
            }
        }
        public static IEnumerable<Assembly> Referenced(this IEnumerable<Assembly> assemblies, IEnumerable<Assembly> referenced)
        {

            foreach (var ass in assemblies)
            {
                if (referenced.Where(o => o == ass).FirstOrDefault() != null)
                {
                    yield return ass;
                }
                else
                {
                    foreach (var refAss in ass.GetReferencedAssemblies())
                    {
                        if (referenced.Where(o => o.FullName == refAss.FullName).FirstOrDefault() != null)
                        {
                            yield return ass;
                            break;
                        }
                    }
                }
            }
        }
    }
}
