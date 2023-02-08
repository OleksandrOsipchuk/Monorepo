using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Room
{
    public interface IRoomManagerFactory
    {
        public IRoomManager CreateRoom(byte typeChoice);
    }
    public interface IRoomManager
    {
        float Temperature { get; }
        bool IsLighted { get; }
    }
    public class RoomManagerFactory
    {
        public IRoomManager CreateRoom(byte typeChoice)
        {
            //logic
        }
    }
}
