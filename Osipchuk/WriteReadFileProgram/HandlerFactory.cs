using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class HandlerFactory
    {
        public IHandler GetHandler(string format)
        {
            IHandler handler;
            if (format == "json")
            {
                handler = new JsonHandler();
            }
            else handler = new XmlHandler();

           return handler;
        }        
    }
}



