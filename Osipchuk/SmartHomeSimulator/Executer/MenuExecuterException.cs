using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Executer
{
    public class MenuExecuterException : Exception
    {
        public MenuExecuterException(string Message) : base(Message) { }
    }
}
