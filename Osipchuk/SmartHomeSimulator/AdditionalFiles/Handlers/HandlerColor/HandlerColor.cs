using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers.Color
{
    public class HandlerColor : IHandlerColor
    {
        public HandlerColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
        public int R { get; }
        public int G { get; }
        public int B { get; }
    }
}
