using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public class RoomBuilder : IRoomBuilder
    {
        private Room _room = new Room();
        public IRoomBuilder ChangeHumidity()
        {
            Console.Write("Write needed humidity: ");
            float humidity = (float)Convert.ToDouble(Console.ReadLine());
            _room.Humidity = humidity; 
            return this;
        }

        public IRoomBuilder ChangeLightState()
        {
            Console.Write("Write needed ligh state: ");
            bool isLighted = Convert.ToBoolean(Console.ReadLine());
           _room.IsLighted = isLighted;
            return this;
        }

        public IRoomBuilder ChangeTemperature()
        {
            Console.Write("Write needed temperature: ");
            float temperature = (float)Convert.ToDouble(Console.ReadLine());
            _room.Temperature = temperature;
            return this;
        }
        public IRoomBuilder ChangeTVState()
        {
            Console.Write("Write needed TV state: ");
            bool isLighted = Convert.ToBoolean(Console.ReadLine());
            _room.IsTVWorking = isLighted;
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
