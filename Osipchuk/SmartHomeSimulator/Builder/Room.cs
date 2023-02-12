using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHomeSimulator.Builder
{
    public class Room
    {
        public float Temperature { get; set; }
        public float? Humidity { get; set; }
        public bool IsLighted { get; set; }
        public bool? IsTVWorking { get; set; }
        public override string ToString()
        {
            string results = "";
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this) != null)
                    results += ($"\n{property.Name}: {property.GetValue(this)}");
            }
            return results;
        }
    }
}
