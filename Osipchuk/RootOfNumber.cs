using SimpleCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk
{
    class RootOfNumber : IAdditionalOperations
    {
        public int UsingNumber { get; set; }
        public RootOfNumber(int usingNumber) 
        {
            UsingNumber = usingNumber;
        }

        public double Operation()
        {
            double result = Math.Sqrt(UsingNumber);
            return result;
        }

        public void PrintResult()
        {
            Console.WriteLine($"The root of the number is: {Operation()} ");
        }
    }
}
