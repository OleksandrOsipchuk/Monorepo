﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonAndXml
{
    public interface IHandler
    {
        public void Write(string name, NestedData info);
        public Data[] ReadAll();
        public  Data Read( int id);
    }
}
