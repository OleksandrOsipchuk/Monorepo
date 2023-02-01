using FileManager.OperationParameters;
using FileValidator.OperationParameters;
using FileWorker.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileWriters
{
    public interface IFileWriteFactory
    {
        IFileWriter Create(string extension);
    }
    public interface IFileWriter
    {
        Task<OperationResult> Write(WriteParameters parameters);
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
