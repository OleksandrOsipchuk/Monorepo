using SmartHomeSimulator.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class Wardrobe : RoomBase, IRoomManager
    {
        public void PrintOpportunities()
        {
            Console.WriteLine("\nYou can turn on/off the light (command: <switchLight>)." +
                "\nYou can turn on/off the TV (command: <temperature>)." +
                "\nYou can get all info about the room (command: <info>)");
        }
        public void DoSomething()
        {
            Console.Write("Write command: ");
            string command = Console.ReadLine();
            switch (command)
            {
                case "switchLight":
                    ChangeLightState();
                    break;
                case "temperature":
                    ChangeTemperature();
                    break;
                case "info":
                    GetAllInfo();
                    break;
            }
        }
    }
}
