using System;

namespace Operation
{
    abstract class ArithmeticOperation 
    {
        protected int FirstNumber { get; set; }
        protected int SecondNumber { get; set; }
        public ArithmeticOperation(int firstNumber, int secondNumber)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
        }

        protected ArithmeticOperation(int firstNumber)
        {
            FirstNumber = firstNumber;
        }

        protected virtual void Calculate() { }
    }
}

