using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public class ConsoleRoomHandler : IRoomHandler
    {
        public void RoomsListOutput(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                Console.WriteLine(room);
            }
            //Console.WriteLine("Enter the index of the room to affect: ");
            //var enumerator = _rooms.GetEnumerator();
            //for (int i = 0; enumerator.MoveNext(); i++)
            //    Console.WriteLine($"{i + 1}: {_rooms[i].Name}");
        }
        public string GetRoomType()
        {
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
        public int GetIndex()
        {
            return int.Parse(Console.ReadLine());
        }
        public string GetProperty()
        {
            Console.WriteLine("Write property name you want to change: ");
            return Console.ReadLine();
        }
        public Room GetRoom(List<Room> rooms)
        {
            int index = int.Parse(Console.ReadLine());
            return rooms[index - 1];
        }
    }
}
