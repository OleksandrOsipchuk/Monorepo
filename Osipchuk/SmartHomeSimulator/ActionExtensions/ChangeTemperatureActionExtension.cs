//using SmartHomeSimulator.Rooms;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartHomeSimulator.ActionExtensions
//{
//    public static class ChangeTemperatureActionExtension
//    {
//        public static ActionResult ChangeTemperature(this RoomBase room, float temperature)
//        {
//            if (temperature >= 10 && temperature <= 35)
//            {
//                room.Temperature = temperature;
//                return new ActionResult($"Temperature is now at: {temperature}°C.", true);
//            }
//            else return new ActionResult($"Temperature didn't change - asked for too high/low temperature(10°C-35°C)", false);
//        }
//    }
//}
