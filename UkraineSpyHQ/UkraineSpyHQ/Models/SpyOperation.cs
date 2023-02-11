using System;

namespace UkraineSpyHQ.Models
{
	public class SpyOperation : IOperation<SpyOperationResult>
	{
        private static readonly Random _random = new();

        public async Task<SpyOperationResult> CompleteOperationAsync()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                var spyFate = _random.Next(1, 4);
                return spyFate switch
                {
                    1 => new SpyOperationResult(true, "Success Spy"), // alive
                    2 => new SpyOperationResult(false, "Spy is dead"), // death
                    _ => throw new Exception($"{nameof(SpyOperationResult)} failed") // failure
                };
            }
            catch (Exception e)
            {
                return new SpyOperationResult(false, e.Message);
            }
        }
    }
}

