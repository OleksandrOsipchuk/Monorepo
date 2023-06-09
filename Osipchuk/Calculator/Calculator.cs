﻿using Osipchuk;
using SimpleCalculator;
using System;
using System.Linq;
using Osipchuk.Operations;

namespace SimpleCalculator
{
    class Calculator
    {
        static void Main()
        {
            Console.WriteLine("Welcome to <Simple Calculator>!!\n");
            bool condition = true;
            while (condition)
            {
                Console.WriteLine("Please choose needed operation: " +
                "\nPress<1> if you want to add numbers. " +
                "\nPress<2> if you want to substract numbers. " +
                "\nPress<3> if you want to multiply numbers. " +
                "\nPress<4> if you want to divide numbers. " +
                "\nPress<5> if you want to raise a number to a power. " +
                "\nPress<6> if you want to get root of number. \n");

                string[] numOfOp = { "1", "2", "3", "4", "5", "6" };
                string operation = Console.ReadLine();
                bool exep = numOfOp.Contains(operation);

                if (!exep)
                {
                    Console.WriteLine("You wrote wrong operation! Try again.\n");
                    continue;
                }
                
                try
                {
                    NumbersReader numbersReader = new NumbersReader();
                    (double number1, double number2) numbers = numbersReader.Read(operation);
                    OperationFactory operationFactory = new OperationFactory();
                    var rez = operationFactory.GetOperation(operation);
                    Console.WriteLine($"Result: {rez.Calculate(numbers.number1, numbers.number2)}");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Your number is too big. Please try another!");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You wrote not number. Please try again!");
                    continue;
                }

                Console.WriteLine("Press <Enter> if you want to continue the program.\n");
                ConsoleKey entr = Console.ReadKey().Key;
                if (entr == ConsoleKey.Enter)
                {
                    condition = true;
                }
                else condition = false;
            }
        }
    }
}