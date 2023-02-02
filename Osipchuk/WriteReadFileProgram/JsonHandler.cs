using Program;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Program
{
    public class JsonHandler :Handler,IHandler
    {
        private string pathForJson = @"D:\NewFolder\db.json";
        public void Write(string name, NestedData info)
        {
            CreateFileIfNotExists(pathForJson);
            IList<Data> allData = ReadAll();
            int currentId;
            if (allData.Count==0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add(new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            string serealizedJson = JsonConvert.SerializeObject(allData);
            File.WriteAllText(pathForJson, serealizedJson);
            Console.WriteLine("Saccess!!");
        }
        public IList<Data> ReadAll()
        {
            if (File.Exists(pathForJson) == false) throw new ReadFromDBException("There is no any data yet.");
            string json = File.ReadAllText(pathForJson);
            IList<Data> dataArray = JsonConvert.DeserializeObject<IList<Data>>(json);

             return dataArray ?? new List<Data>();
           
        }
        public Data Read(int id)
        {          
            IList<Data> dataArray = ReadAll();
            
            Data data = dataArray.FirstOrDefault(d => d.Id == id);
            if (data != null) return data;
            else throw new ReadFromDBException("There is no data with whis id.");
        }      
    }
}
