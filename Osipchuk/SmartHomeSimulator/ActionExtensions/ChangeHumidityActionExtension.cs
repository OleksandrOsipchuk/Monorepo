//using SmartHomeSimulator.Interfaces.IProperties;
//using SmartHomeSimulator.Room;
//using SmartHomeSimulator.Rooms;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text;
//using System.Threading.Tasks;

//namespace SmartHomeSimulator.Action
//{
//    public static class ChangeHumidityActionExtension
//    {
//        public static ActionResult ChangeHumidity(this IHumidity room, float humidity)
//        {
//            if (humidity >= 10 && humidity <= 70)
//            {
//                room.Humidity = humidity;
//                return new ActionResult($"Humidity has changed to {humidity}%.", true);
//            }
//            else return new ActionResult($"Humidity hasn't changed, value should be between 10%-70%.", false);
//        }
//    }
//}
