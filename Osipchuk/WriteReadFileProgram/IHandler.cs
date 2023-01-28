using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAndXml
{
    public interface IHandler
    {
        public void WriteToDb(string name, NestedData info);
        public abstract  List<Data> ReadAllFromDb();
        public  void ReadFromDb( int id);
    }
}
