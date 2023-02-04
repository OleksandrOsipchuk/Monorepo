using System;

namespace FileValidator.OperationParameters
{
    public class ReadParameters : IParameters
    {
        public ReadParameters(string filePath, bool zip)
        {
            FilePath = filePath; // .Substring(1, filePath.Length - 2); on question
            Zip = zip;
        }
        public string FilePath { get; private set; }
        public bool Zip { get; private set; }
    }
}
