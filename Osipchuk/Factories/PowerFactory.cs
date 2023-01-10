using Operation;
using System;
using Osipchuk.Operations;

namespace Osipchuk.Factories
{
    class PowerFactory : OperarionFactory
    {
        public PowerFactory()
        {
            try
            {
                Console.Write("\n  Write your number: ");
                FirstNumber = Convert.ToInt32(Console.ReadLine());

                Console.Write("  Write power you need: ");
                SecondNumber = Convert.ToInt32(Console.ReadLine());
                FacturyMethod();
            }
            catch (OverflowException)
            {
                Console.WriteLine("Your number is too big. Please try another!");
            }
            catch (FormatException)
            {
                Console.WriteLine("You wrote not number. Please try again!");
            }

        }
        protected override ArithmeticOperation FacturyMethod()
        {
            return new Power(FirstNumber, SecondNumber);
        }
    }
}
