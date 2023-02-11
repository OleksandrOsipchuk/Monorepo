using System;
namespace UkraineSpyHQ.Models
{
    public class SpyOperationResult : IOperationResult
    {
        public bool IsSuccess { get; private set; }

        public string Data { get; private set; }

        public SpyOperationResult(bool isSuccess,
            string data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
    }
}

