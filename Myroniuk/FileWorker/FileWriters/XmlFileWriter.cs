using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileWorker.DataReaders;
using FileValidator;
using Newtonsoft.Json;
using System.Xml;

namespace FileWorker.FileWriters
{
    public class XmlFileWriter : DataHelper, IFileWritable
    {
        public async Task<OperationResult> Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                    await sw.WriteAsync(data);
                if (GetZip(request))
                {
                    using (FileStream fileToCompress = File.OpenRead(path))
                    {
                        using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".xml".Length) + ".zip"))
                        {
                            using (ZipArchive archive = await Task.Run(() => new ZipArchive(compressedFile, ZipArchiveMode.Create)))
                            {
                                ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                            }
                        }
                    }
                    File.Delete(path);
                    return new OperationResult(true, $"XML file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".xml".Length) + ".zip"}.");
                }
                return new OperationResult(true, $"Data was written to the \\{path} file successfully.");
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, "The extension of the file is not predicted by the program: " + ex.Message);
            }
            catch (XmlException ex)
            {
                return new OperationResult(false, "The data provided is not valid for XML file" + ex.Message);
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading/writing the file: " + ex.Message);
            }
        }
    }
}
