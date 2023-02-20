using SmartHomeSimulator.AdditionalFiles.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public interface IRoomDirector
    {
        void Build(IIOHandler handler);
    }
}
