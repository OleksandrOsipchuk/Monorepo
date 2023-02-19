using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulator.HousesFiles //Rename later
{
    public class FileJsonWorker : IDataWorker
    {
        private readonly string _path;
        internal static List<House> _houses;

        public FileJsonWorker(string path) { _path = path; }
        public async Task WriteAsync<T>(T obj)
        {
            using (StreamWriter sw = new StreamWriter(_path))
                await sw.WriteAsync(JsonConvert.SerializeObject(obj));
        }
        public async Task<List<T>> ReadAsync<T>()
        {
            using (StreamReader sr = new StreamReader(_path))
                return JsonConvert.DeserializeObject<List<T>>(await sr.ReadToEndAsync()) ?? new List<T> ();
        }
    }
}
