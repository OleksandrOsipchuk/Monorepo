using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.AdditionalFiles.Handlers.IO;
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
        public async Task Build(IIOHandler handler)
        {
            await _builder.SetNameAsync(handler);
            await _builder.AddTemperatureAsync(handler);
            await _builder.AddLightStateAsync(handler);
            await _builder.AddHumidityAsync(handler);
        }
    }
}
