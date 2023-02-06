using FileManager.OperationParameters;
using FileValidator.OperationParameters;
using FileWorker.FileWriters;

namespace FileManager.FileWriters
{
    public interface IFileWriteFactory
    {
        IFileWriter Create(string extension);
    }
    public interface IFileWriter
    {
        Task<OperationResult> WriteAsync(WriteParameters parameters);
    }
    public class FileWriteFactory : IFileWriteFactory
    {
        public IFileWriter Create(string extension)
        {
            switch (extension)
            {
                case ".json":
                    return new JsonFileWriter();
                case ".txt":
                    return new TxtFileWriter();
                case ".xml":
                    return new XmlFileWriter();
                default:
                    throw new ArgumentException("Invalid file extension.");
            }
        }
    }
}
