using System;

namespace SimpleCalculator
{
    class Program
    {
        static void Main()
        {
            bool condition = true;
            while (condition)
            {
                try
                {
                    int firstNumber, secondNumber;
                    string operation;
                    {
                        Console.Write("  Write your first number: ");
                        firstNumber = Convert.ToInt32(Console.ReadLine());

                        Console.Write("  Write your second number: ");
                        secondNumber = Convert.ToInt32(Console.ReadLine());

                        Console.Write("  Write your operation(+ , - , / , *): ");
                        operation = Console.ReadLine();
                    }

                    int result;
                    {
                        switch (operation)
                        {
                            case "+":
                                result = firstNumber + secondNumber;
                                break;
                            case "-":
                                result = firstNumber - secondNumber;
                                break;
                            case "/":
                                result = firstNumber / secondNumber;
                                break;
                            case "*":
                                result = firstNumber * secondNumber;
                                break;
                            default:
                                throw new Exception("You wrote wrong operation! Try again.");
                        }
                        Console.WriteLine($"Result: {result}");
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Exeption: {ex.Message}.");
                    continue;
                }

                //exit the program
                {
                    Console.WriteLine("Press <Enter> if you want to continue the program.");
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
}