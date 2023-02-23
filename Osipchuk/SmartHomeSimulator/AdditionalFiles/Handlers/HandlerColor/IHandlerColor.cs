using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers.Color
{
    public interface IHandlerColor
    {
        int R { get; }
        int G { get; }
        int B { get; }
    }
}
