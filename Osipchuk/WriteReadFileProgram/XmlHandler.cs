using JsonAndXml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.IO;
using System.Runtime.Serialization;

namespace JsonAndXml
{
    public class XmlHandler : IHandler
    {

        static private XmlSerializer formatter = new XmlSerializer(typeof(Data[]));
        private string pathForXml = @"D:\NewFolder\db.xml";
        public void Write(string name, NestedData info)
        {
            CreateFile(pathForXml);
            Data[] allData = ReadAll();
            int currentId;
            if (allData.Length == 0) currentId = -1;
            else currentId = allData.Last().Id;
            Array.Resize(ref allData, currentId + 2);
            allData[allData.Length - 1] = (new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            using (FileStream fStream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fStream, allData);
            }
            Console.WriteLine("Saccess!!");
        }
        public Data[] ReadAll()
        {
            if (File.Exists(pathForXml) == false) throw new ReadFromDBException("There is no any data yet.");
            using (FileStream fstream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {
                Data[] dataArray = formatter.Deserialize(fstream) as Data[];
                return dataArray;
            }
        }
        public Data Read(int id)
        {
            if (File.Exists(pathForXml) == false) throw new ReadFromDBException("There is no any data yet.");
            Data[] dataArray = ReadAll();
            Data data = dataArray.FirstOrDefault(d => d.Id == id);
            if (data != null) return data;
            else throw new ReadFromDBException("There is no data with whis id.");
        }
        private static void CreateFile(string path)
        {
            if (!Directory.Exists(@"D:\NewFolder"))
            {
                Directory.CreateDirectory(@"D:\NewFolder");
            }
            if (File.Exists(path) == false)
            {
                var file = File.Create(path);
                file.Close();
                using (FileStream fStream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fStream, new Data[] {});
                }
            }
        }
    }
}