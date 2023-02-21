using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SmartHomeSimulator.AdditionalFiles;

namespace SmartHomeSimulator.Builder.RoomFiles
{
    public class Room : INameable
    {
        public string Name { get; set; }
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }
        public bool? IsLighted { get; set; }
        public bool? IsTVWorking { get; set; }
        public override string ToString()
        {
            string results = "";
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this) != null)
                    results += $"\n{property.Name}: {property.GetValue(this)}";
                if (property.Name == nameof(Temperature)) results += RoomConstants.CelsiusDegreeSymbol;
                if (property.Name == nameof(Humidity)) results += RoomConstants.PercentSymbol;
            }
            return results;
        }
    }
}
