using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder.RoomFiles
{
    public class InvalidRoomTypeException : Exception
    {
        public InvalidRoomTypeException(string Message) : base(string.Format($"Invalid room type: {Message}")) { }
    }
    public class InvalidRoomValueException : Exception
    {
        public InvalidRoomValueException(string Message) : base(string.Format($"Invalid room-switch value: {Message}")) { }
    }
}
