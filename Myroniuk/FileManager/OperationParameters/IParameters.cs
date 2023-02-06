using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileValidator.OperationParameters
{
    public interface IParameters {
        string FilePath { get; }
        bool isZip { get; }
    }
    public static class ParametersParserExtension
    {
        public static IParameters ParseParameters(this string[] args)
        {
            if(args.Length == 0) { args = Console.ReadLine().Split(' '); }
            string fileName = string.Empty;
            string data = string.Empty;
            bool zip = false;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--filename="))
                {
                    fileName = arg.Split("=")[1].TrimStart('{').TrimEnd('}');
                    if (fileName.Contains(".zip")) zip = true;
                }
                else if (arg.StartsWith("--data="))
                    data = arg.Split("=")[1];
                else if (arg.StartsWith("--zip=true"))
                    zip = true;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                if (!string.IsNullOrEmpty(data))
                    return new WriteParameters(data, fileName, zip);
                else
                    return new ReadParameters(fileName, zip);
            }
            return null;
        }
    }
}
