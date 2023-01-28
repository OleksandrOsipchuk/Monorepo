using JsonAndXml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JsonAndXml
{
    public class JsonHandler : IHandler
    {
        private string pathForJson = @"D:\NewFolder\db.json";
        public void WriteToDb( string name, NestedData info)
        {
            CreateFile(pathForJson);
            List<Data> allData = ReadAllFromDb();
            int currentId;
            if (allData.Count == 0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add(new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            string serealizedJson = JsonConvert.SerializeObject(allData);
            File.WriteAllText(pathForJson, serealizedJson);
            Console.WriteLine("Saccess!!");
        }
        public List<Data> ReadAllFromDb()
        {
            string json = File.ReadAllText(pathForJson);
            List<Data> dataArray = JsonConvert.DeserializeObject<List<Data>>(json);
            return dataArray ?? new List<Data>();
        }
        public void ReadFromDb( int id)
        {
            if (File.Exists(pathForJson) == false) { Console.WriteLine("There are no any data yet."); return; }
            List<Data> dataArray = ReadAllFromDb();
            if (dataArray.Any(data => data.Id == id))
            {
                Data data = dataArray.FirstOrDefault(d => d.Id == id);
                Console.WriteLine($"Name: {data.Name}, Info: {data.Nested.Info}");
            }
            else Console.WriteLine("There is no this Id.");
        }
        private  void CreateFile(string path)
        {
            if (!Directory.Exists(@"D:\NewFolder"))
            {
                Directory.CreateDirectory(@"D:\NewFolder");
            }
            if (File.Exists(path) == false)
            {
                var file = File.Create(path);
                file.Close();
            }
        }
    }
}
//CreateFile(pathForJson);
//List<Data> allData = ReadAllFromDb();
//int currentId;
//if (allData.Count == 0) currentId = -1;
//else currentId = allData.Last().Id;
//allData.Add(new Data(++currentId, name, info));
//Console.WriteLine($"Id: {currentId}");
//string serealizedJson = JsonConvert.SerializeObject(allData);
//File.WriteAllText(pathForJson, serealizedJson);
//Console.WriteLine("Saccess!!");