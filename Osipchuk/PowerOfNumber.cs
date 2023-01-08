using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class PowerOfNumber : IAdditionalOperations
    {
        public int UsingNumber { get; set; }
        public int PowerOfUsingNumber { get; set; }
        public PowerOfNumber(int usingNumber,int powerOfUsingNumber)
        {
            UsingNumber = usingNumber;
            PowerOfUsingNumber= powerOfUsingNumber;
        }
        public double Operation()
        {
            double result = Math.Pow(UsingNumber, PowerOfUsingNumber);
            return result;
        }

        public void PrintResult()
        {
            Console.WriteLine($"The power result is: {Operation()} ");
        }
    }
}
