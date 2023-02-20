using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.RoomFiles;

namespace SmartHomeSimulator.Builder.Directors
{
    public class BathroomDirector : IRoomDirector
    {
        private readonly IRoomBuilder _builder;
        public BathroomDirector(IRoomBuilder builder)
        {
            _builder = builder;
        }
        public void Build(IIOHandler handler)
        {
            _builder
                .SetName(handler)
                .AddTemperature(handler)
                .AddLightState(handler)
                .AddHumidity(handler);
        }
    }
}
