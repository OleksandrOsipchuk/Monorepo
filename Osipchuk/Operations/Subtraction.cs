using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Subtraction : IOperation
    {
        public Subtraction(double firstNumber, double secondNumber)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }
        private double FirstNumber { get; set; }
        private double SecondNumber { get; set; }
        public double Calculate()
        {
            return FirstNumber - SecondNumber;
        }
    }
}
