using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    class OperationExeption : Exception
    {
        public OperationExeption(string Message) : base(Message) { }
    }
}
