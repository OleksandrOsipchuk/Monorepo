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
        public string IsTVWorking { get; set; }
        public string IsLighted { get; set; }

    }
}
