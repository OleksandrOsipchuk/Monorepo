using System;

namespace FileValidator.OperationParameters
{
    public class ReadParameters : IParameters
    {
        public ReadParameters(string filePath, bool zip)
        {
            FilePath = filePath;
            isZip = zip;
        }
        public string FilePath { get; private set; }
        public bool isZip { get; private set; }
    }
}
