using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.AdditionalFiles.Handlers
{
    public interface IIOHandler
    {
        void Write<T>(T message);
        string Read();
    }
}