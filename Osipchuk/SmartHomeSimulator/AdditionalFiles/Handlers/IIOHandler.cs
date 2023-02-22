using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers
{
    public interface IIOHandler
    {
        Task WriteAsync(string message);
        Task<string> ReadAsync();
        void Clear();
        void ChangeForegroundColor(ConsoleColor color);
        void ResetColor();
    }
}