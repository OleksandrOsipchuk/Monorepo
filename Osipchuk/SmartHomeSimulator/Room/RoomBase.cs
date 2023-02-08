using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.Room;

namespace SmartHomeSimulator
{
    public abstract class RoomBase
    {
        public ActionResult ChangeTemperature (IRoomManager room, float temperature)
        {
            //logic
            return new ActionResult($"Temperature is now at: {temperature} degrees Celsium.", true);
        }
        public ActionResult ChangeLightState(IRoomManager room, bool isLighted)
        {
            //logic
            return new ActionResult($"Lights are : {(isLighted ? "ON" : "OFF")}.", true);
        }
    }
}
