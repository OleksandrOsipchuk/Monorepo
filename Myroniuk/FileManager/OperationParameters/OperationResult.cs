using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.OperationParameters
{
    public class OperationResult
    {
        public string Message
        {
            get { return Success ? "Task succeed: \n" + _message : "Task failed: \n" + _message; }
            private set { _message = value; }
        }
        public bool Success { get; }
        private string _message;
        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
