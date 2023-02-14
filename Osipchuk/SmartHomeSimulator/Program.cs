//using SmartHomeSimulator.Action;
//using SmartHomeSimulator.Room;
//using SmartHomeSimulator.Rooms;
//using System;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using Newtonsoft.Json;

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
                try
                {
                    List<House> houses = HouseJSONExtension.HouseDeserialize(@".\houses.json");
                    string directorType = GetType();  // <-- starting here refactor 
                    var builder = new RoomBuilder();
                    RoomDirectorFactory factory = new RoomDirectorFactory();
                    IRoomDirector director = factory.GetRoomDirector(directorType, builder);

                    director.Build();
                    Room room = builder.GetRoom();
                    Console.WriteLine(room);
                    Console.WriteLine($"\nFilds you can change: ");
                    Console.WriteLine(room);
                    var newBuilder = new RoomBuilder(room);
                    ChangeRoom(newBuilder, room);
                    Room newRoom = newBuilder.GetRoom();
                    Console.WriteLine(room); // <-- 
                    houses.HouseSerialize(@".\houses.json");
                }
                catch (RoomExсeption ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong value. Try write in correct format.");
                }
                catch (IOException ex) { Console.WriteLine(ex.Message); }
            }
            
        }       
        private static void ChangeRoom(RoomBuilder builder,Room room)
        {
            string property = Console.ReadLine();
            if (room.CheckIfContain(property))
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
        private static string GetType()
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
    }
}
