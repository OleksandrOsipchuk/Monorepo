using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator
{
    public static class HouseListExtension
    {
        public static void HouseSerialize (this IEnumerable<House> houses)
        {
            using (StreamWriter sw = new StreamWriter(@".\houses.json"))
                sw.Write(JsonConvert.SerializeObject(houses));
        }
        public static IEnumerable<House>? HouseDeserialize (string path)
        {
            using (StreamReader sr = new StreamReader(path))
                return JsonConvert.DeserializeObject<List<House>>(sr.ReadToEnd());
        }
    }
}
