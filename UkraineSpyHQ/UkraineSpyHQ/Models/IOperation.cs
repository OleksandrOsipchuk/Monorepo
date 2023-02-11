using System;
namespace UkraineSpyHQ.Models
{
	public interface IOperation<T>
		where T : IOperationResult
	{
		Task<T> CompleteOperationAsync();
	}
}

