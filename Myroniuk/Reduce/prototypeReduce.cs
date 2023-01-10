using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceApplication
{
    public delegate T AccumulatorDelegate<T>(T accumulator, T currentValue);
    public static class prototypeReduce
    {
        public static T Reduce<T>(this T[] array, AccumulatorDelegate<T> accumulator)
        {
            T accumulatorValue = default(T);
            foreach(T value in array)
            {
                accumulatorValue = accumulator(accumulatorValue, value);
            }
            return accumulatorValue;
        }
    }
}
