using Operation;
using Osipchuk.Operations;
using System;


namespace Osipchuk.Factories
{
    class RootFactory : OperarionFactory
    {
        public RootFactory()
        {
            try
            {
                Console.Write("\n  Write your number: ");
                FirstNumber = Convert.ToInt32(Console.ReadLine());
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
            return new Root(FirstNumber);
        }
    }
}
