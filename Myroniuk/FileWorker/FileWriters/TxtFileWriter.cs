using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWorker.DataReaders;

namespace FileWorker.FileWriters
{
    public class TxtFileWriter : DataHelper, IFileWritable
    {
        public event Action<string>? Notify;
        public void Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            bool zip = GetZip(request);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(data.TrimStart('{').TrimEnd('}'));
                Notify?.Invoke($"Data was written to the \\{path} file successfully.");
            }
            if (zip)
            {
                using (FileStream fileToCompress = File.OpenRead(path))
                {
                    using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".txt".Length) + ".zip"))
                    {
                        using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                        {
                            ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                        }
                    }
                }
                File.Delete(path);
                Notify?.Invoke($"Text file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".txt".Length) + ".zip"}.");
            }
        }
        
    }
}
