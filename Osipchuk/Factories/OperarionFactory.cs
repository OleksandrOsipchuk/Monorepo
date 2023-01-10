using System;
using Operation;


namespace Osipchuk.Factories
{
    abstract class OperarionFactory
    {
        protected int FirstNumber { get; set; }
        protected int SecondNumber { get; set; }
        protected abstract ArithmeticOperation FacturyMethod(); 
    }
}
