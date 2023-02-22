using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.AdditionalFiles.Handlers;
using SmartHomeSimulator.Builder.RoomFiles;

namespace SmartHomeSimulator.Builder.Directors
{
    public class KitchenDIrector : IRoomDirector
    {
        private readonly IRoomBuilder _builder;
        public KitchenDIrector(IRoomBuilder builder)
        {
            _builder = builder;
        }
        public async Task Build(IIOHandler handler)
        {
            await _builder.SetNameAsync(handler);
            await _builder.AddHumidityAsync(handler);
            await _builder.AddTemperatureAsync(handler);
            await _builder.AddLightStateAsync(handler);
        }
    }
}
