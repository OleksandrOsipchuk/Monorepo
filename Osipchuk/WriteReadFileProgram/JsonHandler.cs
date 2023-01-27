using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonAndXml
{
    public static class JsonHandler
    {
        public static void WriteToJsonDb(string path1, string name, NestedData info)
        {
            List<Data> allData = ReadAllFromJsonDb(path1);
            int currentId;
            if (allData.Count == 0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add(new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            string serealizedJson = JsonConvert.SerializeObject(allData);
            File.WriteAllText(path1, serealizedJson);
            Console.WriteLine("Saccess!!");
        }
        private static List<Data> ReadAllFromJsonDb(string path1)
        {
            string json = File.ReadAllText(path1);
            List<Data> dataArray = JsonConvert.DeserializeObject<List<Data>>(json);
            return dataArray ?? new List<Data>();
        }
        public static void ReadFromJsonDb(string path1, int id)
        {
            List<Data> dataArray = ReadAllFromJsonDb(path1);
            if (dataArray.Any(data => data.Id == id))
            {
                Data data = dataArray.FirstOrDefault(d => d.Id == id);
                Console.WriteLine($"Name: {data.Name}, Info: {data.Nested.Info}");
            }
            else Console.WriteLine("There is no this Id.");
        }
    }
}
