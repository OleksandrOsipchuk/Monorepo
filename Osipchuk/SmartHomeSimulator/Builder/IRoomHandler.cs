using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public interface IRoomHandler
    {
        void Write<T>(T message);
        string Read();        
    }
}
