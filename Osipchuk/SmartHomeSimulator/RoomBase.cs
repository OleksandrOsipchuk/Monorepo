using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public abstract class RoomBase
    {
        protected bool TurnOfLite(bool isLiteOn)
        {
            if (!isLiteOn)
            {
                Console.WriteLine("The light was off!!");
                return false;
            }
            Console.WriteLine("The light is already off!!");
            return false;
        }
    }
}
