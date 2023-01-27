using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonAndXml
{
    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NestedData Nested { get; set; }
        public Data(int id, string name, NestedData nested)
        {
            Id = id;
            Name = name;
            Nested = nested;
        }
        public Data() { }       
    }

}
