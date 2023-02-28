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
    public class Room : NameBase
    {
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }
        public bool? IsLighted { get; set; }
        public bool? IsTVWorking { get; set; }
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this) != null) stringBuilder.AppendFormat($"\n{property.Name}: {property.GetValue(this)}");
                if (property.Name == nameof(Temperature)) stringBuilder.Append(RoomConstants.CelsiusDegreeSymbol);
                if (property.Name == nameof(Humidity)) stringBuilder.Append(RoomConstants.PercentSymbol);
            }
            string result = stringBuilder.ToString();
            return result;
        }
    }
}
