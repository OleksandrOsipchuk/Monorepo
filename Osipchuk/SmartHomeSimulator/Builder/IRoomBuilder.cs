using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public interface IRoomBuilder
    {
        IRoomBuilder ChangeName();
        IRoomBuilder ChangeHumidity();
        IRoomBuilder ChangeLightState();
        IRoomBuilder ChangeTemperature();
        IRoomBuilder ChangeTVState();
        Room GetRoom();
    }
}
