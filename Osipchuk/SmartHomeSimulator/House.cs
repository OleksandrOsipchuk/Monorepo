using Newtonsoft.Json;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class House
    {
        private RoomBuilder roomBuilder = new();

        [JsonProperty]
        private List<Room> rooms= new();
        public void AddRoom ()
        {
            var builder = new RoomBuilder();
            RoomDirectorFactory factory = new RoomDirectorFactory();
            IRoomDirector director = factory.GetRoomDirector(GetRoomType(), builder);
            director.Build();
            Room room = builder.GetRoom();
            rooms.Add(room);
        }
        public void RemoveRoom (Room room) => rooms.Remove(room);
        public List<Room> GetRooms () => rooms; // Maybe keep it IEnumerable for readonly purposes. REVIEW LATER!!!
        private string GetRoomType() // should shorten this up for sure
        {
            Console.Clear();
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
    }
}