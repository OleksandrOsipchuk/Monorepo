using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeSimulator.Rooms;

namespace SmartHomeSimulator.Room
{
    public interface IRoomManagerFactory
    {
        public RoomBase CreateRoom(string typeOfRoom);
    }   
}
