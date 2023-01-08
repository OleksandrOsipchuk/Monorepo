using Osipchuk;
using SimpleCalculator;
using System;

namespace SimpleCalculator
{
    class Calculator
    {
        static void Main()
        {
            Console.WriteLine("Welcome to <Simple Calculator>!!\n");
            bool bigCondition = true;
            while (bigCondition)
            {
                Console.WriteLine("Please choose needed operation: " +
                    "\nPress<1> if you need usual arithmetic expression. " +
                    "\nPress<2> if you need to raise a number to a power. " +
                    "\nPress<3> if you need to make root of number.\n");
                ConsoleKey oper = Console.ReadKey().Key;
                if (oper == ConsoleKey.NumPad1)
                {
                    bool smallCondition = true;
                    while (smallCondition)
                    {
                        try
                        {
                            int firstNumber, secondNumber;
                            string operation;
                            {
                                Console.Write("\n  Write your first number: ");
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
                        smallCondition = false;
                        bigCondition = Exit();

                    }
                }

                else if (oper == ConsoleKey.NumPad2)
                {
                    bool smallCondition = true;
                    while (smallCondition)
                    {
                        try
                        {
                            int usingNumber, powerOfNumber;
                            {
                                Console.Write("\n  Write your number: ");
                                usingNumber = Convert.ToInt32(Console.ReadLine());
                                Console.Write("  Write power you need: ");
                                powerOfNumber = Convert.ToInt32(Console.ReadLine());
                            }
                            PowerOfNumber expressionPow = new PowerOfNumber(usingNumber, powerOfNumber);
                            expressionPow.PrintResult();
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
                        smallCondition = false;
                        bigCondition = Exit();
                    }

                }
                else if (oper == ConsoleKey.NumPad3)
                {
                    bool smallCondition = true;
                    while (smallCondition)
                    {
                        try
                        {
                            Console.Write("\n  Write your number: ");
                            int usingNumber = Convert.ToInt32(Console.ReadLine());

                            RootOfNumber epressionRot = new RootOfNumber(usingNumber);
                            epressionRot.PrintResult();
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
                        smallCondition = false;
                        bigCondition = Exit();
                    }
                }
                else Console.WriteLine("\nYou wrote wrong number! Try again..");
            }
            static bool Exit()
            {
                Console.WriteLine("Press <Enter> if you want to continue the program.\n");
                ConsoleKey entr = Console.ReadKey().Key;
                if (entr == ConsoleKey.Enter)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}