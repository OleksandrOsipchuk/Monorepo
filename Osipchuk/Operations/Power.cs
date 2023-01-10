using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Power : ArithmeticOperation
    {
        public Power(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {
            Calculate();
        }
        protected override void Calculate()
        {
            Console.WriteLine($"  Result:  {FirstNumber}  in power  {SecondNumber}  =  {Math.Pow(FirstNumber, SecondNumber)} \n");
        }
    }
}
