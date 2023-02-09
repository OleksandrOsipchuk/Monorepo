using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Room
{
    public class RoomManagerFactory : IRoomManagerFactory
    {
        public IRoomManager CreateRoom(byte typeChoice)
        {
            return new Bedroom();
        }
    }
}
