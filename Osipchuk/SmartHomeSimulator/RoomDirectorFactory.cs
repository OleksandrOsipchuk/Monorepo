using SmartHomeSimulator.Builder;
using SmartHomeSimulator.Builder.Directors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public class RoomDirectorFactory
    {
        public IRoomDirector GetRoomDirector(string typeOfDirector, IRoomBuilder builder)
        {
            return typeOfDirector switch
            {
                "bathroom" => new BathroomDirector(builder),
                "bedroom" => new BedroomDirector(builder),
                "kitchen" => new KitchenDIrector(builder),
                _ => throw new RoomExсeption("There is no this type of room."),
            }; 
           
        }
    }
}
