﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osipchuk
{
    public class NumbersReader
    {
        private double number1, number2;
        private string snum1, snum2;
        public (double number1, double number2) Read(string operation)
        {
            if (operation == "1" || operation == "2" || operation == "3" || operation == "4")
            {
                Console.Write("\n  Write your first number: ");  
                number1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("  Write your second number: ");
                number2 = Convert.ToInt32(Console.ReadLine());  
            }
            else if (operation == "5")
            {
                Console.Write("\n  Write your number: ");
                number1 = Convert.ToInt32(Console.ReadLine());
                Console.Write("  Write power you need: ");
                number2 = Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.Write("\n  Write your number: ");
                number1 = Convert.ToInt32(Console.ReadLine());
            }
            return (number1, number2);
        }
    }
}
