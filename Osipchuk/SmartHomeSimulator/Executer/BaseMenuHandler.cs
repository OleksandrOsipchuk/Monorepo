using SmartHomeSimulator.AdditionalFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.AdditionalFiles.Handlers.IO;
using SmartHomeSimulator.AdditionalFiles.Handlers.Color;

namespace SmartHomeSimulator.Executer
{
    public class BaseMenuHandler
    {
        public static async Task<int> GetOptionAsync<T>(IList<T> values, IIOHandler handler, params string[] args) where T : NameBase 
        {
            await handler.WriteAsync("Select an option: ");
            int i = 1;
            handler.ChangeForegroundColor(new ConsoleColorParameters(0, 255, 0));
            while (i <= values.Count)
            {
                await handler.WriteAsync($"{i}. {values[i - 1].Name}.");
                i++;
            }
            handler.ChangeForegroundColor(new ConsoleColorParameters(0, 255, 255));
            foreach (var arg in args)
                await handler.WriteAsync($"{i++}. {arg}");
            handler.ResetColor();
            await handler.WriteAsync($"{i++}. Return.");
            return int.Parse(await handler.ReadAsync());
        }
    }
}
