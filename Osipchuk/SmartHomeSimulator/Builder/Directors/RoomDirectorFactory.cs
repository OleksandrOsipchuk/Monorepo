using SmartHomeSimulator.Builder.RoomFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.Directors
{
    public class RoomDirectorFactory
    {
        public IRoomDirector GetRoomDirector(RoomType type, IRoomBuilder builder)
        {
            return type switch
            {
                RoomType.Bathroom => new BathroomDirector(builder),
                RoomType.Bedroom => new BedroomDirector(builder),
                RoomType.Kitchen => new KitchenDIrector(builder),
                _ => throw new RoomExсeption("There is no this type of room."),
            };
        }
    }
}
