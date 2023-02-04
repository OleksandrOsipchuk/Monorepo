using Program;
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
using System.Xml;

namespace Program
{
    public class XmlHandler : Handler, IHandler
    {

        private static XmlSerializer formatter = new XmlSerializer(typeof(List<Data>));
        private string pathForXml = @"D:\NewFolder\db.xml";
        public void Write(string name, NestedData info)
        {
            CreateFileIfNotExists(pathForXml);
            IList<Data> allData = ReadAll();
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
        public IList<Data> ReadAll()
        {
            if (File.Exists(pathForXml) == false) throw new ReadFromDBException("There is no any data yet.");
            using (FileStream fstream = new FileStream(pathForXml, FileMode.OpenOrCreate))
            {
                IList<Data> dataArray = formatter.Deserialize(fstream) as List<Data>;
                return dataArray;
            }
        }
        public Data Read(int id)
        {
            if (File.Exists(pathForXml) == false) throw new ReadFromDBException("There is no any data yet.");
            XmlReader reader = XmlReader.Create(pathForXml);
            reader.ReadToDescendant("Data");
            do
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader.ReadOuterXml());
                XmlNode item = doc.DocumentElement;
                XmlSerializer serial = new XmlSerializer(typeof(Data));
                using (XmlNodeReader dataReader = new XmlNodeReader(item))
                {
                    Data data = (Data)serial.Deserialize(dataReader);
                    if (data.Id == id) return data;
                }
            }
            while (reader.ReadToNextSibling("Data"));
            reader.Close();
            throw new ReadFromDBException("There is no data with whis id.");
        }
        protected override void CreateFileIfNotExists(string path)
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
                    formatter.Serialize(fStream, new List<Data>());
                }
            }
        }
    }
}