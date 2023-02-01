using FileWorker.DataReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileValidator.OperationParameters
{
    public class ReadParameters : IParameters
    {
        public ReadParameters(string filePath, bool zip)
        {
            FilePath = filePath;
            Zip = zip;
            Extension = Path.GetExtension(filePath);
        }
        public string FilePath { get; private set; }
        public string Extension { get; private set; }
        public bool Zip { get; private set; }
    }
}
