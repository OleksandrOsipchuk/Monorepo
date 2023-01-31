using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWorker.DataReaders;
using FileValidator;

namespace FileWorker.FileWriters
{
    public class JsonFileWriter : DataHelper, IFileWriter
    {
        async public Task<OperationResult> Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            try
            {
                JToken.Parse(data);
                    using (StreamWriter sw = new StreamWriter(path))
                        await sw.WriteAsync(JsonConvert.SerializeObject(data));
                if(GetZip(request))
                {
                    using (FileStream fileToCompress = File.OpenRead(path))
                    using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".json".Length) + ".zip"))
                    using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }
                    File.Delete(path);
                    return new OperationResult(true, $"JSON file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".json".Length) + ".zip"}.");
                }
                return new OperationResult(true, $"Data was written to the \\{path} file successfully.");
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, "The extension of the file is not predicted by the program: " + ex.Message);
            }
            catch (JsonReaderException ex)
            {
                return new OperationResult(false, "The data provided is not in the correct format for a json file: " + ex.Message);
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading/writing the file: " + ex.Message);
            }
        }
    }
}
