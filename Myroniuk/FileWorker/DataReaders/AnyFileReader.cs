using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker.DataReaders
{
    public class AnyFileReader : DataHelper
    {
        public event Action<string>? Notify;
        public void Read(string request)
        {
            string path = GetFilename(request);
            bool zip = GetZip(request);
            try
            {
                if (zip)
                {
                    using (ZipArchive archive = ZipFile.OpenRead(path))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            using (StreamReader sr = new StreamReader(entry.Open()))
                            {
                                Notify?.Invoke(sr.ReadToEnd());
                            }
                        }
                    }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string fileContent = sr.ReadToEnd();
                        Notify?.Invoke(fileContent);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Notify?.Invoke("The file could not be found: " + ex.Message);
            }
            catch (IOException ex)
            {
                Notify?.Invoke("An error occurred while reading the file: " + ex.Message);
            }
            catch (Exception ex)
            {
                Notify?.Invoke("Unexpected exception: " + ex.Message);
            }
        }
    }
}
