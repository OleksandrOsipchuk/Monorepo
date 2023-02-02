using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public abstract class Handler
    {
        protected virtual void CreateFileIfNotExists(string path)
        {
            if (!Directory.Exists(@"D:\NewFolder"))
            {
                Directory.CreateDirectory(@"D:\NewFolder");
            }
            if (File.Exists(path) == false)
            {
                var file = File.Create(path);
                file.Close();
            }
        }
    }
}
