using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class ActionResult
    {
        public ActionResult(string msg)
        {
            Message = msg;
        }
        public string Message
        {
            get
            {
                return "Success! " + message;
            }
            set
            {
                message += value;
            }
        }
        private string message;
    }
}
