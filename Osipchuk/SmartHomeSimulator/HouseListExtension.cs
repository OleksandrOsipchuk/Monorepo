using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public static class HouseJSONExtension
    {
        public static void HouseSerialize (this IEnumerable<House> houses, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.Write(JsonConvert.SerializeObject(houses));
        }
        public static List<House> HouseDeserialize (string path)
        {
            using (StreamReader sr = new StreamReader(path))
                return JsonConvert.DeserializeObject<List<House>>(sr.ReadToEnd()) ?? new List<House>();
        }
    }
}
