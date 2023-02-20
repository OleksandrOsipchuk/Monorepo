using SmartHomeSimulator.AdditionalFiles.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.RoomFiles
{
    public class RoomBuilder : IRoomBuilder
    {
        private Room _room = new Room();
        public RoomBuilder() { }
        public IRoomBuilder SetName(IIOHandler handler)
        {
            handler.Write("Write room name: ");
            string Name = handler.Read() ?? "Room";
            _room.Name = Name;
            return this;
        }
        public IRoomBuilder AddHumidity(IIOHandler handler)
        {
            handler.Write("Write needed humidity(from 30 to 60): ");
            float humidity = float.Parse(handler.Read() ?? "30");
            if (humidity < 30 || humidity > 60)
                throw new RoomExсeption("Wrong humidity value! Try another..");
            _room.Humidity = humidity;
            return this;
        }
        public IRoomBuilder AddTemperature(IIOHandler handler)
        {
            handler.Write("Write needed temperature(from 0 to 40): ");
            float temperature = float.Parse(handler.Read() ?? "15");
            if (temperature < 0 || temperature > 40)
                throw new RoomExсeption("Wrong temperature value! Try another..");
            _room.Temperature = temperature;
            return this;
        }
        public IRoomBuilder AddLightState(IIOHandler handler)
        {
            handler.Write("Write room light state(On/Off): ");
            string command = handler.Read() ?? "Off";
            if (command != "On" && command != "Off")
                throw new RoomExсeption("Wrong command! Try another..");
            _room.IsLighted = command == "On" ? true : false;
            return this;
        }
        public IRoomBuilder AddTVState(IIOHandler handler)
        {
            handler.Write("Write room TV state(On/Off): ");
            string command = handler.Read() ?? "Off";
            if (command != "On" && command != "Off")
                throw new RoomExсeption("Wrong command! Try another..");
            _room.IsTVWorking = command == "On" ? true : false;
            return this;
        }
        public Room GetRoom()
        {
            Room room = _room;
            _room = new();
            return room;
        }
    }
}
