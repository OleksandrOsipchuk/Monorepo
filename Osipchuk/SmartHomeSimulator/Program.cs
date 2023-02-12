//using SmartHomeSimulator.Action;
//using SmartHomeSimulator.Room;
//using SmartHomeSimulator.Rooms;
//using System;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;

namespace SmartHomeSimulator // rename (?)
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new RoomBuilder();
            RoomDirectorFactory factory = new RoomDirectorFactory();
            IRoomDirector director = factory.GetRoomDirector("bedroom", builder);
            director.Build();
            Room room = builder.GetRoom();
            RoomPrinter printer= new RoomPrinter();
            printer.PrintRoom(room);

            //            //Console.WriteLine("Hello! Welcome to Smart home.");
            //            //bool isWork = true;
            //            //while (isWork)
            //            //{
            //            //    string typeOfRoom;
            //            //    try { typeOfRoom = GetType(); }
            //            //   catch(RoomExсeption ex)
            //            //    {
            //            //        Console.WriteLine(ex.Message);
            //            //        continue;
            //            //    }

            //            //    RoomManagerFactory factory = new RoomManagerFactory();
            //            //    IRoomAction room = factory.CreateRoom(typeOfRoom);
            //            //    room.PrintOpportunities();
            //            //    room.DoSomething();


            //            //}
        }
        //        //private static string GetType()
        //        //{
        //        //    Console.WriteLine("Choose needed room!" +
        //        //        "\nWrite < bathroom > to work with Bathroom. " +
        //        //        "\nWrite < bedroom > to work with Bedroom. " +
        //        //        "\nWrite < kitchen > to work with Kitchen. " +
        //        //        "\nWrite < living > to work with Living room. " +
        //        //        "\nWrite < wardrobe > to work with Wardrobe. " +
        //        //        "\nWrite < corridor > to work with Сorridor. ");
        //        //    string[] rooms = { "bathroom", "bedroom", "kitchen", "living", "wardrobe", "corridor" };
        //        //    string room = Console.ReadLine();
        //        //    if (rooms.Contains(room)) return room;
        //        //    else throw new RoomExсeption("There is no this commad!! Try another...");
        //        //}
    }
}
