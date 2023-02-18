using Newtonsoft.Json;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.HousesFiles
{
    public class House : ConsoleRoomHandler, INameable
    {
        public string Name { get; set; }
        public House(string name) { Name = name; }
        [JsonProperty]
        private List<Room> _rooms = new();
        public List<Room> GetRooms() => _rooms;
        private Room currentRoom;
        public void AddRoom()
        {
            var builder = new RoomBuilder();
            RoomDirectorFactory factory = new RoomDirectorFactory();
            IRoomDirector director = factory.GetRoomDirector(GetRoomType(), builder);
            director.Build();
            Room room = builder.GetRoom();
            _rooms.Add(room);
        }
        public void RemoveRoom()
        {
            RoomsListOutput(_rooms);
            currentRoom = GetRoom(_rooms);
            _rooms.Remove(currentRoom);
        }
        public void ChangeRoom() 
        {
            currentRoom = GetRoom(_rooms);
            var builder = new RoomBuilder(currentRoom);
            string property = GetProperty();
            if (currentRoom.CheckIfContain(property))
                switch (property)
                {
                    case nameof(currentRoom.Temperature):
                        builder.ChangeTemperature();
                        break;
                    case nameof(currentRoom.Humidity):
                        builder.ChangeHumidity();
                        break;
                    case nameof(currentRoom.IsLighted):
                        builder.ChangeLightState();
                        break;
                    case nameof(currentRoom.IsTVWorking):
                        builder.ChangeTVState();
                        break;
                }
            else throw new RoomExсeption("There is no such field");
        }       
    }
}