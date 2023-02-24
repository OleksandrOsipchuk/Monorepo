using SmartHomeSimulator.AdditionalFiles.Handlers.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers.IO
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

        public void Clear()
        {
            Console.Clear();
        }
        public void ResetColor()
        {
            Console.ResetColor();
        }
        public void ChangeForegroundColor(IColorParameters color)
        {
            int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0; // Bright bit
            index |= (color.R > 64) ? 4 : 0; // Red bit
            index |= (color.G > 64) ? 2 : 0; // Green bit
            index |= (color.B > 64) ? 1 : 0; // Blue bit
            Console.ForegroundColor= (ConsoleColor)(index);
        }
    }
}