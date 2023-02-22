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
        public async Task<IRoomBuilder> SetNameAsync(IIOHandler handler)
        {
            await handler.WriteAsync("Write room name: ");
            string Name = await handler.ReadAsync();
            _room.Name = Name;
            return this;
        }
        public async Task<IRoomBuilder> AddHumidityAsync(IIOHandler handler)
        {
            await handler.WriteAsync("Write needed humidity(from 30 to 60): ");
            float humidity = float.Parse(await handler.ReadAsync());
            if (humidity < 30 || humidity > 60)
                throw new InvalidRoomValueException("Wrong humidity value! Try another..");
            _room.Humidity = humidity;
            return this;
        }
        public async Task<IRoomBuilder> AddTemperatureAsync(IIOHandler handler)
        {
            await handler.WriteAsync("Write needed temperature(from 0 to 40): ");
            float temperature = float.Parse(await handler.ReadAsync());
            if (temperature < 0 || temperature > 40)
                throw new InvalidRoomValueException("Wrong temperature value! Try another..");
            _room.Temperature = temperature;
            return this;
        }
        public async Task<IRoomBuilder> AddLightStateAsync(IIOHandler handler)
        {
            await handler.WriteAsync("Write room light state(On/Off): ");
            string command = await handler.ReadAsync();
            if (command != "On" && command != "Off")
                throw new InvalidRoomValueException("Wrong command! Try another..");
            _room.IsLighted = command == "On" ? true : false;
            return this;
        }
        public async Task<IRoomBuilder> AddTVStateAsync(IIOHandler handler)
        {
            await handler.WriteAsync("Write room TV state(On/Off): ");
            string command = await handler.ReadAsync();
            if (command != "On" && command != "Off")
                throw new InvalidRoomValueException("Wrong command! Try another..");
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
