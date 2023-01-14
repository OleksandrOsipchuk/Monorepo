using SimpleCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    public class Power : IOperation
    {
        public double Calculate(double number1, double number2)
        {
            return Math.Pow(number1,number2);
        }
    }
}
