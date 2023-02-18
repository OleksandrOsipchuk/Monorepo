//using SmartHomeSimulator.Action;
//using SmartHomeSimulator.Room;
//using SmartHomeSimulator.Rooms;
//using System;
using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using Newtonsoft.Json;
using SmartHomeSimulator.HousesFiles;
/////////////////////////////////////////////////////////////////
namespace SmartHomeSimulator // rename (?)
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var worker = new FileJsonWorker(@".\houses.json");
            var exec = new MenuExecuter(worker);
            await exec.RunAsync();
            //var exec = new MenuExecuter(new FileJsonWorker(@".\house.json")); //wrong path for testing
            //_ = exec.RunAsync();
            //Console.WriteLine("Hello! Welcome to Smart home.");
            //bool isWork = true;
            //while (isWork)
            //{
            //    try
            //    {

            //        //string directorType = GetType(); //get rid of this and just pass GetType() as argiment in House  // <-- starting here refactor 
            //        //var builder = new RoomBuilder();
            //        //RoomDirectorFactory factory = new RoomDirectorFactory();
            //        //IRoomDirector director = factory.GetRoomDirector(directorType, builder);
            //        //director.Build();
            //        //Room room = builder.GetRoom();

            //        //var newBuilder = new RoomBuilder(room);
            //        //ChangeRoom(newBuilder, room);
            //        //Room newRoom = newBuilder.GetRoom();
            //        //Console.WriteLine(room); // <-- 
            //        //houses.HouseSerialize(@".\houses.json");
            //    }
            //    catch (RoomExсeption ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        continue;
            //    }
            //    catch (FormatException)
            //    {
            //        Console.WriteLine("Wrong value. Try write in correct format.");
            //    }
            //    catch (IOException ex) { Console.WriteLine(ex.Message); }
            //}

        }
    }
}
