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
        public string Name { get; set; }
        public House(string name) { Name = name; }
        [JsonProperty]
        private List<Room> _rooms = new();
        public List<Room> GetRooms() => _rooms;
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
            RoomsListOutput();
            int index = int.Parse(Console.ReadLine());
            _rooms.Remove(_rooms[index - 1]);
        }
        public void ChangeRoom() // maybe do instead of index - room.name
            // also rework this 100%
        {
            RoomsListOutput();
            int index = int.Parse(Console.ReadLine());
            var builder = new RoomBuilder(_rooms[index - 1]);
            Console.WriteLine("Write property name you want to change: ");
            string property = Console.ReadLine();
            if (_rooms[index - 1].CheckIfContain(property))
                switch (property)
                {
                    case "Temperature":
                        builder.ChangeTemperature();
                        break;
                    case "Humidity":
                        builder.ChangeHumidity();
                        break;
                    case "IsLighted":
                        builder.ChangeLightState();
                        break;
                    case "IsTVWorking":
                        builder.ChangeTVState();
                        break;
                }
            else throw new RoomExсeption("There is no such field");
        }
        private string GetRoomType() // should shorten this up for sure
        {
            //Thread.Sleep(500);
            //Console.Clear();
            Console.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><>");
            Console.WriteLine("Choose needed room!" +
                "\nWrite < bathroom > to work with Bathroom. " +
                "\nWrite < bedroom > to work with Bedroom. " +
                "\nWrite < kitchen > to work with Kitchen. " +
                "\nWrite < living > to work with Living room. " +
                "\nWrite < wardrobe > to work with Wardrobe. " +
                "\nWrite < corridor > to work with Сorridor. ");
            string[] rooms = { "bathroom", "bedroom", "kitchen", "living", "wardrobe", "corridor" };
            string room = Console.ReadLine();
            return rooms.Contains(room) ? room : throw new RoomExсeption("There is no this commad!! Try another...");
        }
        private void RoomsListOutput()
        {
            foreach (var room in _rooms)
            {
                Console.WriteLine(room);
            }
            //Console.WriteLine("Enter the index of the room to affect: ");
            //var enumerator = _rooms.GetEnumerator();
            //for (int i = 0; enumerator.MoveNext(); i++)
            //    Console.WriteLine($"{i + 1}: {_rooms[i].Name}");
        }
    }
}