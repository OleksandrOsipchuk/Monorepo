using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.Builder
{
    public class Room
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public bool IsTVWorking { get; set; }
        public bool IsLighted { get; set; }

    }
}
