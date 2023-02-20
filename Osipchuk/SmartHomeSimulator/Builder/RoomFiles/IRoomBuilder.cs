using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.RoomFiles
{
    public interface IRoomBuilder
    {
        IRoomBuilder SetName();
        IRoomBuilder AddHumidity();
        IRoomBuilder AddLightState();
        IRoomBuilder AddTemperature();
        IRoomBuilder AddTVState();
        Room GetRoom();
    }
}
