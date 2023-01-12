using Osipchuk.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk
{
    class OperationFactory
    {
        private IOperation rez;

        public IOperation GetOperation(string operation)
        {
            var taker = new NumbersReader();
            switch (operation)
            {
                case "1":
                    rez = new Addition();
                    break;
                case "2":
                    rez = new Subtraction();
                    break;
                case "3":
                    rez = new Multiplication();
                    break;
                case "4":
                    rez = new Division();
                    break;
                case "5":
                    rez = new Power();
                    break;
                case "6":
                    rez = new Root();
                    break;
                    
            }
            return rez;
        }

    }
}
