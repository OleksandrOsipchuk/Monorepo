using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Root : ArithmeticOperation
    {
        public Root(int firstNumber) : base(firstNumber)
        {
            Calculate();
        }
        protected override void Calculate()
        {
            Console.WriteLine($"  Result: sqeare root of {FirstNumber} = {Math.Sqrt(FirstNumber)}\n");
        }
    }
}
