using System;
namespace UkraineSpyHQ.Models
{
	public class ThiefOperationResult : IOperationResult
	{
		public ThiefOperationResult(bool isSuccess,
			string data)
		{
            IsSuccess = isSuccess;
            Data = data;
        }

        public bool IsSuccess { get; private set; }

        public string Data { get; private set; }
    }
}

