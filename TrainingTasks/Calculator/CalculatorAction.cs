using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class CalculatorAction
    {
        public void Calculate(int a, char op, int b)
        {
            switch (op)
            {
                case '+':
                    {
                        Console.WriteLine($"The Sum of {a} and {b} =\t{a + b}");
                        break;
                    }
                case '-':
                    {
                        Console.WriteLine($"The Substract of {a} and {b} =\t{a - b}");
                        break;
                    }
                case '*':
                    {
                        Console.WriteLine($"The Multipication of {a} and {b} =\t{a * b}");
                        break;
                    }
                case '/':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Division of {a} on {b} =\t{a / b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot divide by zero.");
                        }
                        break;
                    }
                case '%':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Mod of {a} on {b} =\t{a % b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot perform Modulus by zero");
                        }
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Invalid operator! Try again."); break;
                    }
            }
        }
        public void Calculate(float a, char op, float b)
        {
            switch (op)
            {
                case '+':
                    {
                        Console.WriteLine($"The Sum of {a} and {b} =\t{a + b}");
                        break;
                    }
                case '-':
                    {
                        Console.WriteLine($"The Substract of {a} and {b} =\t{a - b}");
                        break;
                    }
                case '*':
                    {
                        Console.WriteLine($"The Multipication of {a} and {b} =\t{a * b}");
                        break;
                    }
                case '/':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Division of {a} on {b} =\t{a / b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot divide by zero.");
                        }
                        break;
                    }
                case '%':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Mod of {a} on {b} =\t{a % b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot perform Modulus by zero");
                        }
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Invalid operator! Try again."); break;
                    }
            }
        }
        public void Calculate(double a, char op, double b)
        {

            switch (op)
            {
                case '+':
                    {
                        Console.WriteLine($"The Sum of {a} and {b} =\t{a + b}");
                        break;
                    }
                case '-':
                    {
                        Console.WriteLine($"The Substract of {a} and {b} =\t{a - b}");
                        break;
                    }
                case '*':
                    {
                        Console.WriteLine($"The Multipication of {a} and {b} =\t{a * b}");
                        break;
                    }
                case '/':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Division of {a} on {b} =\t{a / b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot divide by zero.");
                        }
                        break;
                    }
                case '%':
                    {
                        if (b != 0)
                        {
                            Console.WriteLine($"The Mod of {a} on {b} =\t{a % b}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot perform Modulus by zero");
                        }
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Invalid operator! Try again."); break;
                    }
            }
        }
        public char ReadValidOperator()
        {
            while (true)
            {
                Console.Write("Enter Operator (+, -, *, /, %):\t");

                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && input.Length == 1)
                {
                    char op = input[0];

                    if (op == '+' || op == '-' || op == '*' || op == '/' || op == '%')
                    {
                        return op;
                    }
                }
                Console.WriteLine("Invalid Operator! Please try again.");
            }
        }
    }
}
