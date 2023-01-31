using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileValidator;
using FileWorker.DataReaders;
using Newtonsoft.Json;

namespace FileWorker.FileWriters
{
    public class TxtFileWriter : DataHelper, IFileWriter
    {
        async public Task<OperationResult> Write(string request)
        {
            string data = GetData(request);
            string path = GetFilename(request);
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                    await sw.WriteAsync(data.TrimStart('{').TrimEnd('}'));
                if (GetZip(request))
                {
                    using (FileStream fileToCompress = File.OpenRead(path))
                    using (FileStream compressedFile = File.Create(path.Substring(0, path.Length - ".txt".Length) + ".zip"))
                    using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                    {
                        ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(path, Path.GetFileName(path));
                    }
                    File.Delete(path);
                    return new OperationResult(true, $"Text file \\{path} was archived successfully to \\{path.Substring(0, path.Length - ".txt".Length) + ".zip"}.");
                }
                return new OperationResult(true, $"Data was written to the \\{path} file successfully.");
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
