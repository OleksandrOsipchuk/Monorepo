using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileWorker.DataReaders;
using FileValidator.OperationParameters;
using FileManager.FileWriters;
using FileManager.OperationParameters;

namespace FileWorker.FileWriters
{
    public class JsonFileWriter : IFileWriter
    {
        async public Task<OperationResult> Write(WriteParameters parameters)
        {
            try
            {
                JToken.Parse(parameters.Data);
                    using (StreamWriter sw = new StreamWriter(parameters.FilePath))
                        await sw.WriteAsync(JsonConvert.SerializeObject(parameters.Data));
                if(parameters.Zip)
                {
                    using (FileStream fileToCompress = File.OpenRead(parameters.FilePath))
                    using (FileStream compressedFile = File.Create(parameters.FilePath.Replace(".json", ".zip")))
                    using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(parameters.FilePath, Path.GetFileName(parameters.FilePath));
                    }
                    File.Delete(parameters.FilePath);
                    return new OperationResult(true, $"\\{parameters.FilePath} archived to \\{parameters.FilePath.Replace(".json", ".zip")}");
                }
                return new OperationResult(true, $"Data was written to the \\{parameters.FilePath} file successfully.");
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
