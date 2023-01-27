using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonAndXml
{
    public class NestedData
    {
        public string Info { get; set; }
        public NestedData(string info)
        {
            Info = info;
        }
        public NestedData() { }
    }
}
