using jsonAndXml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;

namespace jsonAndXml
{
    static class XmlHandler
    {
        
        static private XmlSerializer formatter = new XmlSerializer(typeof(List<Data>));
        public static void WriteToXmlDb(string path2, string name, NestedData info)
        {            
            List<Data> allData = ReadAllFromXmlDb(path2);
            int currentId;
            if (allData.Count == 0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add(new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            using (FileStream fStream = new FileStream(path2, FileMode.OpenOrCreate))
            {        
                 formatter.Serialize(fStream,allData);               
            }
            Console.WriteLine("Saccess!!");
        }
        private static List<Data> ReadAllFromXmlDb(string path2)
        {
            //{костиль
            using (FileStream fStream = new FileStream(path2, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fStream, new List<Data>());
            }
            //}
            using (FileStream fstream = new FileStream(path2, FileMode.OpenOrCreate))
            {
                
                List<Data>? dataArray = formatter.Deserialize(fstream) as List<Data>;
                return dataArray ?? new List<Data>();
            }           
        }
        public static void ReadFromXmlDb(string path1, int id)
        {
            List<Data> dataArray = ReadAllFromXmlDb(path1);
            if (dataArray.Any(data => data.Id == id))
            {
                Data data = dataArray.FirstOrDefault(d => d.Id == id);
                Console.WriteLine($"Name: {data.Name}, Info: {data.Nested.Info}");
            }
            else Console.WriteLine("There is no this Id.");
        }
    }
}
