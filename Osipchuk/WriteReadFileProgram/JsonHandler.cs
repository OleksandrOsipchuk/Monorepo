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
        public void Write(string name, NestedData info)
        {
            CreateFile(pathForJson);
            Data[] allData = ReadAll();
            int currentId;
            if (allData.Length == 0) currentId = -1;
            else currentId = allData.Last().Id;
            Array.Resize(ref allData, currentId+2);
            allData[allData.Length-1] = (new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            string serealizedJson = JsonConvert.SerializeObject(allData);
            File.WriteAllText(pathForJson, serealizedJson);
            Console.WriteLine("Saccess!!");
        }
        public Data[] ReadAll()
        {
            if (File.Exists(pathForJson) == false) throw new ReadFromDBException("There is no any data yet.");
            string json = File.ReadAllText(pathForJson);
            Data[] dataArray = JsonConvert.DeserializeObject<Data[]>(json);
            return dataArray ?? new Data[]{};
        }
        public Data Read(int id)
        {
            Data[] dataArray = ReadAll();
            Data data = dataArray.FirstOrDefault(d => d.Id == id);
            if (data != null) return data;
            else throw new ReadFromDBException("There is no data with whis id.");
        }
        private void CreateFile(string path)
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
