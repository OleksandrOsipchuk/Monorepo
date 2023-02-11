using System;
using UkraineSpyHQ.Models;

namespace UkraineSpyHQ
{
	public class HeadQuarters
	{
        public delegate void
			OperationsHandler(IEnumerable<IOperationResult> operationResults);
        public event OperationsHandler? OperationResultsReady;

		private IList<Task<IOperationResult>>? _operations;
		private readonly Random _random = new();
		public Task StartOperationsAsync()
		{
            _operations = new List<Task<IOperationResult>>();

			for (var i = 0; i < 10; i++)
			{
                _operations.Add(StartOperationAsync());
			}

			Task.WhenAll(_operations).ContinueWith(async task =>
			{
				var results = await task;
				OperationResultsReady?.Invoke(results);
			});

			return Task.CompletedTask;
		}

		private async Task<IOperationResult> StartOperationAsync()
		{
			await Task.Delay(100);

			var operationType = _random.Next(1, 3);
            return operationType switch
            {
                1 => await new SpyOperation().CompleteOperationAsync(),
                _ => await new ThiefOperation().CompleteOperationAsync(),
            };
        }
	}
}

