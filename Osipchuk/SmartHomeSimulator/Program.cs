using SmartHomeSimulator.Room;
using System;
namespace SmartHomeSimulator // rename (?)
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to Smart home.");
            bool isWork = true;
            while (isWork)
            {
                string typeOfRoom;
                try { typeOfRoom = GetType(); }
               catch(RoomExсeption ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                RoomManagerFactory factory = new RoomManagerFactory();
                IRoomManager room = factory.CreateRoom(typeOfRoom);
                room.PrintOpportunities();
                room.DoSomething();


            }
        }


        private static string GetType()
        {
            Console.WriteLine("Choose needed room!" +
                "\nWrite < bathroom > to work with Bathroom. " +
                "\nWrite < bedroom > to work with Bedroom. " +
                "\nWrite < kitchen > to work with Kitchen. " +
                "\nWrite < living > to work with Living room. " +
                "\nWrite < wardrobe > to work with Wardrobe. " +
                "\nWrite < corridor > to work with Сorridor. ");
            string[] rooms = { "bathroom", "bedroom", "kitchen", "living", "wardrobe", "corridor" };
            string room = Console.ReadLine();
            if (rooms.Contains(room)) return room;
            else throw new RoomExсeption("There is no this commad!! Try another...");
        }
    }
}
