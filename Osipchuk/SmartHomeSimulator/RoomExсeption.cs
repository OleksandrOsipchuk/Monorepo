using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class RoomExсeption:Exception
    {
        public RoomExсeption(string Message) : base(Message) { }
    }
}
