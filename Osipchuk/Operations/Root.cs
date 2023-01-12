using SimpleCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    public class Root : IOperation
    {
        (double first, double second) numbers = new NumbersReader().Read(Calculator.operation);
        public double Calculate()
        {        
            return Math.Sqrt(numbers.first);
        }
    }
}
