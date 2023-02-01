using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileValidator.OperationParameters
{
    public interface IParametersFactory
    {
        public IParameters ParseParameters(string[] args);
    }
    public interface IParameters { }
    public class ParametersFactory : IParametersFactory {
        public IParameters ParseParameters(string[] args)
        {
            string fileName = string.Empty;
            string data = string.Empty;
            bool zip = false;
            foreach (var arg in args)
            {
                if (arg.StartsWith("--filename="))
                    fileName = Regex.Match(arg, @"(?<=filename=\{)[^}]+").Value;
                else if (arg.StartsWith("--data="))
                    data = Regex.Match(arg, @"data=\{([^}]+)\}").Groups[1].Value;
                else if (arg == "[--zip=true]")
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
