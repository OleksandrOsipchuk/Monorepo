using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Room
{
    public interface IRoomManagerFactory
    {
        public IRoomManager CreateRoom(string typeOfRoom);
    }
    public interface IRoomManager
    {
        void PrintOpportunities();
        void DoSomething();
    }   
}
