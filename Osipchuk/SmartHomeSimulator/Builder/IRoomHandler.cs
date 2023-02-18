using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public interface IRoomHandler
    {
        string GetRoomType();
        int GetIndex();
        void RoomsListOutput(List<Room> rooms);
        string GetProperty();
        Room GetRoom(List<Room> rooms);
    }
}
