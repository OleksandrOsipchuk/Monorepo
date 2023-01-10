using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk
{
    class TakerOfNumbers
    {
        public double Number1 { get; private set; } 
        public double Number2 { get; private set; }
        public void Taker(string operation)
        {
           
            try
            {
                if (operation == "1" || operation == "2" || operation == "3" || operation == "4")
                {
                    Console.Write("\n  Write your first number: ");
                    Number1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("  Write your second number: ");
                    Number2 = Convert.ToInt32(Console.ReadLine());
                }
                else if (operation == "5")
                {
                    Console.Write("\n  Write your number: ");
                    Number1 = Convert.ToInt32(Console.ReadLine());
                    Console.Write("  Write power you need: ");
                    Number2 = Convert.ToInt32(Console.ReadLine());
                }
                else 
                {
                    Console.Write("\n  Write your number: ");
                    Number1 = Convert.ToInt32(Console.ReadLine());
                }
                 
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
    }
}
