using SmartHomeSimulator.AdditionalFiles.Handlers.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public interface IRoomDirector
    {
        Task Build(IIOHandler handler);
    }
}
