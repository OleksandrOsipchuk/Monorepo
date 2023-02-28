using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceApplication
{
    public static class ContainerExtensions
    {
        public static T Reduce<T>(this IEnumerable<T> items, Func<T,T,T> func)
        {
            var enumerator = items.GetEnumerator();
            if (!enumerator.MoveNext())
                return default(T);
            T accumulator = (T)enumerator.Current;
            while (enumerator.MoveNext())
            {
                accumulator = func(accumulator, enumerator.Current);
            }
            return accumulator;
        }
    }
}
