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
                    foreach (Room room in houses[0].GetRooms()) {
                        Console.WriteLine(room);
                    }
                    houses[0].AddRoom();
                    houses[0].AddRoom();
                    houses[0].AddRoom();
                    //string directorType = GetType(); //get rid of this and just pass GetType() as argiment in House  // <-- starting here refactor 
                    //var builder = new RoomBuilder();
                    //RoomDirectorFactory factory = new RoomDirectorFactory();
                    //IRoomDirector director = factory.GetRoomDirector(directorType, builder);

                    //director.Build();
                    //Room room = builder.GetRoom();
                    //var newBuilder = new RoomBuilder(room);
                    //ChangeRoom(newBuilder, room);
                    //Room newRoom = newBuilder.GetRoom();
                    //Console.WriteLine(room); // <-- 
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
    }
}
