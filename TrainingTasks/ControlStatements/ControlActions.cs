using System;
using System.Collections.Generic;
using System.Text;

namespace ControlStatements
{
    public class ControlActions
    {
        public bool EvenOdd(int number)
        {
            if (number != 0 && number % 2 == 0)
                return true;
            else
                return false;
        }
        public bool IsPrime(int n)
        {
            //for positive numbers
            if (n < 2) return false;

            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        public void Factorial(int input)
        {
            int factorial = 1;
            for (int i = 1; i <= input; i++)
            {
                factorial = factorial * i;
            }
            Console.WriteLine($"The factorial of {input}=\t{factorial}");
        }

        public void NumberTable(int input)
        {
            if (input > 0)
            {
                Console.WriteLine($"The table of {input} as follow:");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"{i}\t*\t{input}=\t{i * input}");
                }
            }
        }

        public void ExecuteAuthentication()
        {
            string password;
            do
            {
                Console.Write("Please Enter a valid password:\t");
                password = Console.ReadLine();
                // let's suppose we have a password "zanair"
                if (password != "zanair")
                {
                    Console.WriteLine("Incorrect password, Please try again.\n");
                }
            }
            while (password != "zanair");
            Console.WriteLine("You have successfully loged into the system to perform desired actions.");
        }
    }
}
