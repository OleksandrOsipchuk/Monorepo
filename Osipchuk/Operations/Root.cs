using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Root : IOperation
    {
        public Root(double number)
        {
            Number = number;
        }
        private double Number { get; set; }
        public double Calculate()
        {
            return  Math.Sqrt(Number);
        }
    }
}
