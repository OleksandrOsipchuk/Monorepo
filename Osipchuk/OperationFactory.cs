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
            var taker = new TakerOfNumbers();
            switch (operation)
            {
                case "1":
                    taker.Taker(operation);
                    rez = new Addition(taker.Number1, taker.Number2);
                    break;
                case "2":
                    taker.Taker(operation);
                    rez = new Subtraction(taker.Number1, taker.Number2);
                    break;
                case "3":
                    taker.Taker(operation);
                    rez = new Multiplication(taker.Number1, taker.Number2);
                    break;
                case "4":
                    taker.Taker(operation);
                    rez = new Division(taker.Number1, taker.Number2);
                    break;
                case "5":
                    taker.Taker(operation);
                    rez = new Power(taker.Number1, taker.Number2);
                    break;
                case "6":
                    taker.Taker(operation);
                    rez = new Root(taker.Number1);
                    break;
                    
            }
            return rez;
        }

    }
}
