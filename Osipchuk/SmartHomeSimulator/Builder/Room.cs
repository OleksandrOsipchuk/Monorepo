using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SmartHomeSimulator.Builder
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
                    results += ($"\n{property.Name}: {property.GetValue(this)}");
                if (property.Name == nameof(Temperature)) results += "°C";
                if (property.Name == nameof(Humidity)) results += "%";
            }
            return results;
        }
        public bool CheckIfContain(string neededProperty)
        {
            return GetType().GetProperties()
                .Where(property => property.GetValue(this) != null)
                .Select(property => property.Name).Contains(neededProperty);
        }
    }
}
