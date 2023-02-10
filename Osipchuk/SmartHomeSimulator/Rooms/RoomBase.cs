using SmartHomeSimulator.Interfaces.IProperties;
using System;

namespace SmartHomeSimulator.Rooms
{
    public abstract class RoomBase
    {
        public float Temperature { get; set; } = 20;
        public bool IsLighted { get; set; } = false;
        public string AvailableActions ()
        {
            string actions = "1. Change temperature.\n" +
                "2. Turn on/off light. \n";
            var roomType = GetType();
            if (roomType.GetInterfaces().Contains(typeof(IHumidity))) actions += "3. Humidity change";
            if (roomType.GetInterfaces().Contains(typeof(ITVState))) actions += "4. TV state change";
            return actions;
        }
    }
}

