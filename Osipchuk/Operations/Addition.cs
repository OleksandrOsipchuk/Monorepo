using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Addition : IOperation
    {
        public Addition(double firstNumber, double secondNumber)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }
        private double FirstNumber { get; set; } = 20;
        private double SecondNumber { get; set; } = 20;
        public double Calculate()
        {
            return FirstNumber+ SecondNumber;
        }
    }
}