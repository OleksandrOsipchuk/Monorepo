using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.AdditionalFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Executer
{
    public class BaseMenuHandler
    {
        public static async Task<int> GetOption<T>(List<T> values, IIOHandler handler, params string[] args) where T : INameable 
        {
            await handler.WriteAsync("Select an option: ");
            int i = 1;
            Console.ForegroundColor = ConsoleColor.Green;
            while (i <= values.Count)
            {
                await handler.WriteAsync($"{i}. {values[i - 1].Name}.");
                i++;
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            foreach (var arg in args)
                await handler.WriteAsync($"{i++}. {arg}");
            Console.ResetColor();
            await handler.WriteAsync($"{i++}. Return.");
            return int.Parse(await handler.ReadAsync());
        }
    }
}
