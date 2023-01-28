using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileWorker.DataReaders;

namespace FileWorker.FileWriters
{
    public class XmlFileWriter : DataHelper, IFileWritable
    {
        public event Action<string>? Notify;
        public void Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            bool zip = GetZip(request);
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            using (StreamWriter sw = new StreamWriter(path))
            {
                serializer.Serialize(sw, data);
                Notify?.Invoke($"Data was written to the \\{path} file successfully.");
            }
            if (zip)
            {
                using (FileStream fileToCompress = File.OpenRead(path))
                {
                    using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".xml".Length) + ".zip"))
                    {
                        using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                        {
                            ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                        }
                    }
                }
                File.Delete(path);
                Notify?.Invoke($"XML file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".xml".Length) + ".zip"}.");
            }
        }
    }
}
