using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class ActionResult
    {
        public ActionResult(string msg, bool actionSuccess) {
            Message = msg;
            isSuccess= actionSuccess;
        }
        public string Message {
            get 
            {
                return isSuccess ? "Success! " + message : "Failed! " + message;
            }
            set
            {
                message += value;
            }
        }
        private string message;
        private bool isSuccess;
    }
}
