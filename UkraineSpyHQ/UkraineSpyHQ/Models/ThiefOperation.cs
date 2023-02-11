using System;

namespace UkraineSpyHQ.Models
{
	public class ThiefOperation : IOperation<ThiefOperationResult>
	{
        private static readonly Random _random = new();

        public async Task<ThiefOperationResult> CompleteOperationAsync()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                var thiefFate = _random.Next(1, 4);
                return thiefFate switch
                {
                    1 => new ThiefOperationResult(true, "Some data"), // success
                    2 => new ThiefOperationResult(false, "Thief is dead"), // death
                    _ => throw new Exception($"{nameof(ThiefOperationResult)} failed") // failure
                };
            }
            catch (Exception e)
            {
                return new ThiefOperationResult(false, e.Message);
            }
        }
    }
}

