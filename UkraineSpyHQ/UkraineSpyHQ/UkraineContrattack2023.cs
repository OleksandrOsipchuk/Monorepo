using UkraineSpyHQ;
using UkraineSpyHQ.Models;

internal class UkraineContrattack2023
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var headQuarters = new HeadQuarters();
        headQuarters.OperationResultsReady += HandleResults;
        await headQuarters.StartOperationsAsync();
        Console.ReadKey();
    }

    private static void HandleResults(IEnumerable<IOperationResult> results)
    {
       foreach (var r in results)
       {
            Console.WriteLine($"Operation result: {r.GetType().Name} " +
                $"IsSuccess: {r.IsSuccess}, Data: {r.Data}");
       }
    }
}