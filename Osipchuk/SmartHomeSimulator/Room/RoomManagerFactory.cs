using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Room
{
    public class RoomManagerFactory : IRoomManagerFactory
    {
        public IRoomManager CreateRoom(string typeOfRoom)
        {
            IRoomManager room;
            switch (typeOfRoom)
            {
                case "bathroom":
                    room = new Bathroom();
                    break;
                case "bedroom":
                    room = new Bedroom();
                    break;
                case "kitchen":
                    room = new Kitchen();
                    break;
                case "living":
                    room = new LivingRoom();
                    break;
                case "wardrobe":
                    room = new Wardrobe();
                    break;
                case "corridor":
                    room = new Сorridor();
                    break;
                default:
                    throw new RoomExсeption("");
            }
            return room;
        }
    }
}
