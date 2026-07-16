using System;
using System.Collections.Generic;
using System.Text;

namespace ArithmaticOperation
{
    public class Operations
    {
        public void Addition(int a, int b)
        {
            Console.WriteLine( a + b);
        }
        public void Substraction(int a, int b)
        {
            Console.WriteLine(a - b);
        }
        public void Multipication(int a, int b)
        {
            Console.WriteLine(a * b);
        }
        public void Division(int a, int b)
        {
            if(b != 0)
            {

                Console.WriteLine(a / b);
            }
            else
            {
                Console.WriteLine("Cannot divide by zero.");
            }
        }
        public void MOD(int a, int b)
        {
            if(b != 0)
            {
                Console.WriteLine(a % b);
            }
            else
            {
                Console.WriteLine("Cannot perform Modulus by zero.");
            }
        }
        public void Increment(int a, int b)
        {
            a++;
            b++;
            Console.WriteLine($"The first increment No={ a} \n The second incremented No={ b}");
        }
        public void Decrement(int a, int b)
        {
            a--;
            b--;
            Console.WriteLine($"The first decrement No={ a} \nThe second decremented No={ b}");
        }

    }
}
