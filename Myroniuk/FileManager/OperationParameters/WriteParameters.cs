using FileWorker.DataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileValidator.OperationParameters
{
    public class WriteParameters : IParameters
    {
        public WriteParameters(string data, string filePath, bool zip)
        {
            Data = data;
            FilePath = filePath;
            Zip = zip;
            Extension = Path.GetExtension(FilePath);
        }
        public string Data { get; private set; }
        public string FilePath { get; private set; }
        public string Extension { get; private set; }
        public bool Zip { get; private set; }
    }
}
