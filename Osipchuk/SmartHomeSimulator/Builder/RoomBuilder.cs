using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public class RoomBuilder : IRoomBuilder
    {
        private Room _room = new Room();
        public RoomBuilder() { }
        public RoomBuilder(Room room)
        {
            _room = room;
        }       
        public IRoomBuilder ChangeHumidity()
        {
            Console.Write("Write needed humidity(from 30 to 60): ");
            float humidity = (float)Convert.ToDouble(Console.ReadLine());
            if (humidity < 30 || humidity > 60)
                throw new RoomExсeption("Wrong humidity value!Try another..");
            _room.Humidity = humidity;
            return this;
        }
        public IRoomBuilder ChangeTemperature()
        {
            Console.Write("Write needed temperature(from 0 to 40): ");
            float temperature = (float)Convert.ToDouble(Console.ReadLine());
            if (temperature < 0 || temperature > 40)
                throw new RoomExсeption("Wrong temperature value!Try another..");
            _room.Temperature = temperature;
            return this;
        }
        public IRoomBuilder ChangeLightState()
        {
            Console.Write("Write needed ligh state(On or Off): ");
            string command = Console.ReadLine();
            if (command != "On" && command != "Off")
                throw new RoomExсeption("Wrong command !Try another..");
            _room.IsLighted = command == "On" ? true : false;
            return this;
        }
        public IRoomBuilder ChangeTVState()
        {
            Console.Write("Write needed TV state(On or Off): ");
            string command = Console.ReadLine();
            if (command != "On" && command != "Off")
                throw new RoomExсeption("Wrong command !Try another..");
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
