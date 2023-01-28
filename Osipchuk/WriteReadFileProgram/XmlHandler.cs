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

        static private XmlSerializer formatter = new XmlSerializer(typeof(List<Data>));
        private string pathForXml = @"D:\NewFolder\db.xml";
        public void WriteToDb(string name, NestedData info)
        {
            CreateFile(pathForXml);
            List<Data> allData = ReadAllFromDb();
            int currentId;
            if (allData.Count == 0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add(new Data(++currentId, name, info));
            Console.WriteLine($"Id: {currentId}");
            using (FileStream fStream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fStream, allData);
            }
            Console.WriteLine("Saccess!!");
        }
        public List<Data> ReadAllFromDb()
        { 
            ///
            using (FileStream fileStream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {

                formatter.Serialize(fileStream, new List<Data>());
            }
            ///
            using (FileStream fstream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {               
                List<Data>? dataArray = formatter.Deserialize(fstream) as List<Data>;
                return dataArray ?? new List<Data>();
            }
        }
        public void ReadFromDb( int id)
        {
            if (File.Exists(pathForXml) == false) { Console.WriteLine("There are no any data yet."); return; }
            List<Data> dataArray = ReadAllFromDb();
            if (dataArray.Any(data => data.Id == id))
            {
                Data data = dataArray.FirstOrDefault(d => d.Id == id);
                Console.WriteLine($"Name: {data.Name}, Info: {data.Nested.Info}");
            }
            else Console.WriteLine("There is no this Id.");
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
            }
        }
    }
}