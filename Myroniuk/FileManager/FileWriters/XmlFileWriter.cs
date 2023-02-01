using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FileWorker.DataReaders;
using Newtonsoft.Json;
using System.Xml;
using FileValidator.OperationParameters;
using FileManager.FileWriters;
using FileManager.OperationParameters;

namespace FileWorker.FileWriters
{
    public class XmlFileWriter : IFileWriter
    {
        public async Task<OperationResult> Write(WriteParameters parameters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            try
            {
                using (StreamWriter sw = new StreamWriter(parameters.FilePath))
                    await sw.WriteAsync(parameters.Data);
                if (parameters.Zip)
                {
                    using (FileStream fileToCompress = File.OpenRead(parameters.FilePath))
                        using (FileStream compressedFile = File.Create(parameters.FilePath.Replace(".xml", ".zip")))
                            using (ZipArchive archive = await Task.Run(() => new ZipArchive(compressedFile, ZipArchiveMode.Create)))
                            {
                                ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(parameters.FilePath, Path.GetFileName(parameters.FilePath));
                            }
                    File.Delete(parameters.FilePath);
                    return new OperationResult(true, $"\\{parameters.FilePath} archived to \\{parameters.FilePath.Replace(".xml", ".zip")}");
                }
                return new OperationResult(true, $"Data was written to the \\{parameters.FilePath} file successfully.");
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
