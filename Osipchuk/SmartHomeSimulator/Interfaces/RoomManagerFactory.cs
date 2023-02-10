using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.Rooms;

namespace SmartHomeSimulator.Room
{
    public class RoomManagerFactory : IRoomManagerFactory
    {
        public RoomBase CreateRoom(string typeOfRoom)
        {
            switch (typeOfRoom)
            {
                case "bathroom":
                    return new BathRoom();
                //case "bedroom":
                //    return new Bedroom();
                //case "kitchen":
                //    return new Kitchen();
                //case "living":
                //    return new LivingRoom();
                //case "wardrobe":
                //    return new Wardrobe();
                //case "corridor":
                //    return  new Сorridor();
                default:
                    throw new RoomExсeption("");
            }
        }
    }
}
