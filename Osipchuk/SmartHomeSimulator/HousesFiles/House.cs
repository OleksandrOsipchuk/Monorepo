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
    public class House : INameable
    {
        ConsoleHandler handler = new ConsoleHandler();
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
            RoomsListOutput(_rooms);
            currentRoom = GetRoom(_rooms);
            var builder = new RoomBuilder(currentRoom);
            handler.Write("Write property name you want to change: ");
            string property = handler.Read();
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
        private string GetRoomType()
        {
            handler.Write("<><><><><><><><><><><><><><><><><><><><><><><><><><>");
            handler.Write("Choose needed room!" +
                "\nWrite < bathroom > to work with Bathroom. " +
                "\nWrite < bedroom > to work with Bedroom. " +
                "\nWrite < kitchen > to work with Kitchen. " +
                "\nWrite < living > to work with Living room. " +
                "\nWrite < wardrobe > to work with Wardrobe. " +
                "\nWrite < corridor > to work with Сorridor. ");
            string[] rooms = { "bathroom", "bedroom", "kitchen", "living", "wardrobe", "corridor" };
            string room = handler.Read();
            return rooms.Contains(room) ? room : throw new RoomExсeption("There is no this commad!! Try another...");
        }
        private void RoomsListOutput(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                handler.Write(room);
            }
        }
        private Room GetRoom(List<Room> rooms)
        {
            int index = int.Parse(handler.Read());
            return rooms[index - 1];
        }
    }
}