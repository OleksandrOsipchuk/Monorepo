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
        public float Temperature { get; set; }
        public bool IsLighted { get; set; }

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
        public void PrintOpportunities()
        {
            Console.WriteLine("You can turn on/off the TV (command: <switch>)");
        }
        public void DoSomething()
        {
            Console.Write("Write command: ");
            string command = Console.ReadLine();

            switch (command)
            {
                case "switch":
                    SwitchTV();
                    break;
            }
        }
    }
}
