using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileWorker.DataReaders
{
    public abstract class DataHelper
    {
        public bool GetZip(string request)
        {
            return request.Contains(" [--zip=true]") || request.Contains(".zip}");
        }
        public string GetFilename(string request)
        {
            return Regex.Match(request, @"(?<=filename=\{)[^}]+").Value;
        }
        public string GetData(string request)
        {
            int dataStartIndex = request.IndexOf(" --data=") + " --data=".Length;
            int dataEndIndex = request.IndexOf(" --filename=");
            return request.Substring(dataStartIndex, dataEndIndex - dataStartIndex);
        }
    }
}
