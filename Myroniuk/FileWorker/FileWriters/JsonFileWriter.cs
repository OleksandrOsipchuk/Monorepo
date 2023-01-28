using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWorker.DataReaders;

namespace FileWorker.FileWriters
{
    public class JsonFileWriter : DataHelper, IFileWritable
    {
        public event Action<string>? Notify;
        public void Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            bool zip = GetZip(request);
            JToken.Parse(data);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(JsonConvert.SerializeObject(data));
                Notify?.Invoke($"Data was written to the \\{path} file successfully.");
            }
            /*maybe i could create Zip Method in DataHelper class and just inherit it in
            all Writers since its code almost the same but idk is it even nice idea with the abstract class*/
            if (zip)
            {
                using (FileStream fileToCompress = File.OpenRead(path))
                {
                    using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".json".Length) + ".zip"))
                    {
                        using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                        {
                            ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                        }
                    }
                }
                File.Delete(path);
                Notify?.Invoke($"JSON file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".json".Length) + ".zip"}.");
            }
        }
    }
}
