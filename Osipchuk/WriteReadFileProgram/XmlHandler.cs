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

namespace Program
{
    public class XmlHandler :Handler, IHandler
    {

        static private XmlSerializer formatter = new XmlSerializer(typeof(List<Data>));
        private string pathForXml = @"D:\NewFolder\db.xml";
        public void Write(string name, NestedData info)
        {
            CreateFileIfNotExists(pathForXml);
            IList<Data> allData = ReadAll();
            int currentId;
            if (allData.Count == 0) currentId = -1;
            else currentId = allData.Last().Id;
            allData.Add( new Data(++currentId, name, info));
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
            IList<Data> dataArray = ReadAll();
            Data data = dataArray.FirstOrDefault(d => d.Id == id);
            if (data != null) return data;
            else throw new ReadFromDBException("There is no data with whis id.");
        }
       override protected  void CreateFileIfNotExists(string path)
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