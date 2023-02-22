using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers
{
    public class ConsoleHandler : IIOHandler
    {
        public async Task WriteAsync(string message)
        {
            using (var writer = new StreamWriter(Console.OpenStandardOutput()))
            {
                await writer.WriteLineAsync(message);
            }
        }
        public async Task<string> ReadAsync()
        {
            using (var reader = new StreamReader(Console.OpenStandardInput()))
            {
                return await reader.ReadLineAsync();
            }
        }
    }
}