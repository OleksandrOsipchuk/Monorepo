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
        public static async Task<int> GetOption<T>(IList<T> values, IIOHandler handler, params string[] args) where T : NameBase 
        {
            await handler.WriteAsync("Select an option: ");
            int i = 1;
            handler.ChangeForegroundColor(ConsoleColor.Green);
            while (i <= values.Count)
            {
                await handler.WriteAsync($"{i}. {values[i - 1].Name}.");
                i++;
            }
            handler.ChangeForegroundColor(ConsoleColor.DarkMagenta);

            foreach (var arg in args)
                await handler.WriteAsync($"{i++}. {arg}");
            handler.ResetColor();
            await handler.WriteAsync($"{i++}. Return.");
            return int.Parse(await handler.ReadAsync());
        }
    }
}
