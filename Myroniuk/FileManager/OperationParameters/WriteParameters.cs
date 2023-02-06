using System;

namespace FileValidator.OperationParameters
{
    public class WriteParameters : IParameters
    {
        public WriteParameters(string data, string filePath, bool zip)
        {
            Data = data;
            FilePath = filePath;
            isZip = zip;
            Extension = Path.GetExtension(FilePath);
        }
        public string Data { get; private set; }
        public string FilePath { get; private set; }
        public string Extension { get; private set; }
        public bool isZip { get; private set; }
    }
}
