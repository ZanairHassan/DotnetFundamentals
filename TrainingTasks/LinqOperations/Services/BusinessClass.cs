using System;
using System.Collections.Generic;
using System.Text;

namespace LinqOperations.Services
{
    public class BusinessClass
    {
        public static int ReadInt()
        {
            int result;

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Console.Write("Invalid number. Please enter a valid integer:\t");
                    continue;
                }

                if (result < 18 || result > 30)
                {
                    Console.Write("Please enter a number between 18 and 30:\t");
                    continue;
                }

                return result;
            }
        }

        public static void Pause()
        {
            Console.WriteLine();

            Console.Write("Press any key...");
            Console.ReadKey();
        }

        public static void Show()
        {
            Console.Clear();

            Console.WriteLine("====================================");
            Console.WriteLine("        LINQ BASICS DEMO");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Where()");
            Console.WriteLine("2. Select()");
            Console.WriteLine("3. OrderBy()");
            Console.WriteLine("4. FirstOrDefault()");
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an option:\t");
        }


    }
}
