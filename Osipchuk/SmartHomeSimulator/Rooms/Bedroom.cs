using SmartHomeSimulator.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class Bedroom : RoomBase, IRoomManager
    {

        private bool IsTVOn = false;
        private void SwitchTV() 
        {
            if (IsTVOn == false)
            {
                IsTVOn = true;
                Console.WriteLine("TV was turned on");
            }
            else Console.WriteLine("TV was turned off");
        }

        protected override void GetAllInfo()
        {
            Console.WriteLine($"Temperature is {Temperature} degrees Celsius." +
               $"\nIs lighted in room: {IsLighted}." +
               $"\nIs TV on: {IsTVOn}.");
        }
        public void PrintOpportunities()
        {
            Console.WriteLine("\nYou can turn on/off the light (command: <switchLight>)." +
                "\nYou can turn on/off the TV (command: <temperature>)."+
                "\nYou can get all info about the room (command: <info>)"+
                "\nYou can turn on/off the TV (command: <switchTV>)." );
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
                case "switchTV":
                    SwitchTV();
                    break;
            }
        }
    }
}
