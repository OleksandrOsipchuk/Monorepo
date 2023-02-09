using SmartHomeSimulator.Room;
using System;
namespace SmartHomeSimulator // rename (?)
{
    class Program
    {
        static void Main(string[] args)
        { 
            RoomManagerFactory factory = new RoomManagerFactory();
            IRoomManager room = factory.CreateRoom(1);
            room.PrintOpportunities();
            room.DoSomething();
        }
    }
}
