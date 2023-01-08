using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    interface IAdditionalOperations
    {
        int UsingNumber { get; set; }
        int Operation();
        int PrintResult();
    }
}
