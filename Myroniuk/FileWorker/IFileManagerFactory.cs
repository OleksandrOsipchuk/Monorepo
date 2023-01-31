using FileValidator;
using FileWorker.FileWriters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker
{
    public interface IFileWriteableFactory
    {
        IFileWritable Create(string extension);
    }
    public interface IFileWritable
    {
        Task<OperationResult> Write(string request);
    }
    public class FileManagerFactory : IFileWriteableFactory
    { 
        public IFileWritable Create(string extension)
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
