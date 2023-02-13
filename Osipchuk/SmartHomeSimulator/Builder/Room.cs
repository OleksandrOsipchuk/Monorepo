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
        public string Name { get; set; }
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
                if (property.Name == "Temperature") results += " degrees Celsius.";
                if (property.Name == "Humidity") results += " %.";
            }
            return results;
        }
        public bool CheckIfContain(string neededProperty)
        {
            List<string> changebleProperties = new List<string>();
            Type type = typeof(Room);
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this) != null) changebleProperties.Add(property.Name);
            }
            return changebleProperties.Contains(neededProperty);
        }
    }
}
