using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.Room;

namespace SmartHomeSimulator
{
    public abstract class RoomBase
    {
        protected float Temperature { get; set; } = 20;
        protected bool IsLighted { get; set; } = false;
        protected virtual void GetAllInfo()
        {
            Console.WriteLine($"Temperature is {Temperature} degrees Celsius." +
                $"\nIs lighted in room: {IsLighted}.");
        }
        public void ChangeTemperature() 
        {
            Temperature = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Temperature is {Temperature}.");
        }
        public void ChangeLightState() 
        {
            if (IsLighted == false)
            {
                IsLighted = true;
                Console.WriteLine("Ligth was turned on");
            }
            else
            {
                IsLighted = false;
                Console.WriteLine("Ligth was turned off");
            }
        }     
    }
}

