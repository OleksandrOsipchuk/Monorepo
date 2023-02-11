using System;
namespace UkraineSpyHQ.Models
{
	public interface IOperationResult
	{
		bool IsSuccess { get; }
		string Data { get; }
	}
}

