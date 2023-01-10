using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Multiplication : ArithmeticOperation
    {
        public Multiplication(int firstNumber, int secondNumber) : base(firstNumber, secondNumber)
        {
            Calculate();
        }
        protected override void Calculate()
        {
            Console.WriteLine($"  Result: {FirstNumber} * {SecondNumber} = {FirstNumber * SecondNumber}\n");
        }
    }
}
