using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk.Operations
{
    class Power : IOperation
    {
        public Power(double number, double powerOfNum)
        {
            Number = number;
            PowerOfNum = powerOfNum;
        }
        private double Number { get; set; }
        private double PowerOfNum { get; set; }
        public double Calculate()
        {
            return Math.Pow(Number, PowerOfNum);
        }
    }
}
