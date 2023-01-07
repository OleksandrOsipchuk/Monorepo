using System;

namespace SimpleCalculator
{
    class Calculator
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
                            throw new OperationExeption("You wrote wrong operation! Try again.");
                    }
                    Console.WriteLine($"Result: {result}");
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
                catch (OperationExeption ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }


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