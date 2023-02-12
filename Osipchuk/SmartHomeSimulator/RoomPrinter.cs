using SmartHomeSimulator.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class RoomPrinter
    {     
        public void PrintRoom(Room room)
        {
            Console.Write($"Temperature: {room.Temperature}. Is lighted: {room.IsLighted}. ");
            if (room.Humidity != null) Console.Write($"Humidity: {room.Humidity}.");
            if (room.IsTVWorking != null) Console.Write($"Is TV On: {room.IsTVWorking}.");
        }
    }
}
