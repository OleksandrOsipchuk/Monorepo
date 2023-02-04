using FileManager.OperationParameters;
using System.IO.Compression;

namespace FileManager.FileWriters
{
    abstract public class FileWriterBase
    {
        public OperationResult WriteArchive (string filepath, string extension)
        {
                using (FileStream fileToCompress = File.OpenRead(filepath))
                using (FileStream compressedFile = File.Create(filepath.Replace(extension, ".zip")))
                using (ZipArchive archive = new ZipArchive(compressedFile, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry archiveEntry = archive.CreateEntryFromFile(filepath, Path.GetFileName(filepath));
                }
                File.Delete(filepath);
                return new OperationResult(true, $"\\{filepath} archived to \\{filepath.Replace(extension, ".zip")}");
        }
    }
}
