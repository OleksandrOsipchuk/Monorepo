using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileValidator
{
    public class OperationResult
    {
        public OperationResult(bool success, string message)
        {
            Success=success;
            Message = message;
        }
        public bool Success { get; }
        public string Message
        {
            get { return Success ? "Task succeed: \n" + _message : "Task failed: \n" + _message; }
            private set { _message = value; }
        }
        private string _message;
    }
}
