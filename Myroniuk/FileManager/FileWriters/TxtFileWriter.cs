using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.FileWriters;
using FileManager.OperationParameters;
using FileValidator.OperationParameters;
using FileWorker.DataReaders;
using Newtonsoft.Json;

namespace FileWorker.FileWriters
{
    public class TxtFileWriter : IFileWriter
    {
        async public Task<OperationResult> Write(WriteParameters parameters)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(parameters.FilePath))
                    await sw.WriteAsync(parameters.Data.TrimStart('{').TrimEnd('}'));
                if (parameters.Zip)
                {
                    using (FileStream fileToCompress = File.OpenRead(parameters.FilePath))
                    using (FileStream compressedFile = File.Create(parameters.FilePath.Replace(".txt", ".zip")))
                    using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(parameters.FilePath, Path.GetFileName(parameters.FilePath));
                    }
                    File.Delete(parameters.FilePath);
                    return new OperationResult(true, $"\\{parameters.FilePath} archived to \\{parameters.FilePath.Replace(".txt", ".zip")}");
                }
                return new OperationResult(true, $"Data was written to the \\{parameters.FilePath} file successfully.");
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, "The extension of the file is not predicted by the program: " + ex.Message);
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading/writing the file: " + ex.Message);
            } 
        }
        
    }
}
