using SmartHomeSimulator.Interfaces.IProperties;
using SmartHomeSimulator.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Action
{
    public static class ChangeLightStateActionExtension
    {
        public static ActionResult ChangeLightState(this RoomBase room)
        {
            room.IsLighted = !room.IsLighted;
            return new ActionResult($"Lights are now: {(room.IsLighted ? "ON" : "OFF")}.", true);
        }
    }
}
