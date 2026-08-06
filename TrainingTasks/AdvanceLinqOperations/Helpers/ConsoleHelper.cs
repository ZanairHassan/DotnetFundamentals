using AdvanceLinqOperations.Models;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLinqOperations.Helpers
{
    public static class ConsoleHelper
    {
        public static int ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out  result))
            {
                Console.Write("Invalid number, try again:\t");
            }

            return result;
        }

        public static decimal ReadDecimal()
        {
            decimal result;
            while (!decimal.TryParse(Console.ReadLine(), out  result))
            {
                Console.Write("Invalid number, try again:\t");
            }

            return result;
        }

        public static double ReadDouble()
        {
            double result;
            while (!double.TryParse(Console.ReadLine(), out  result))
            {
                Console.Write("Invalid number, try again:\t");
            }

            return result;
        }

        public static DateTime ReadDate()
        {
            DateTime result;
            while (!DateTime.TryParse(Console.ReadLine(), out  result))
            {
                Console.Write("Invalid date, try again (yyyy-MM-dd):\t");
            }

            return result;
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void PrintHeader(string title)
        {
            Console.Clear();

            Console.WriteLine(new string('=', 40));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 40));
        }

        public static void PrintEmployees(IEnumerable<Employee> employees)
        {
            if (employees == null || !employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            Console.WriteLine();

            Console.WriteLine(
                $"{"Id",-5}" +
                $"{"Name",-25}" +
                $"{"Department",-12}" +
                $"{"City",-15}" +
                $"{"Salary",-15}" +
                $"{"Experience",-12}" +
                $"{"Active",-8}");
            Console.WriteLine(new string('*', 100));

            foreach (var employee in employees)
            {
                Console.WriteLine(
                    $"{employee.Id,-5}" +
                    $"{employee.FullName,-25}" +
                    $"{employee.DepartmentId,-12}" +
                    $"{employee.City,-15}" +
                    $"{employee.Salary,-15:C}" +
                    $"{employee.Experience,-12}" +
                    $"{(employee.IsActive ? "Yes" : "No"),-8}");
            }

            Console.WriteLine(new string('-', 100));
            Pause();
        }

        public static void PrintStrings(IEnumerable<string> values)
        {
            Console.WriteLine();

            foreach (var value in values)
            {
                Console.WriteLine(value);
            }
        }
    }
}
