using StudentManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StudentManagementSystem.Helpers;

public static class ConsoleHelper
{
    #region Read Input

    public static string ReadString(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            Console.WriteLine("Input cannot be empty.");
        }
    }

    public static string? ReadOptionalString(string message)
    {
        Console.Write(message);
        return Console.ReadLine()?.Trim();
    }

    public static string KeepExistingValue(
    string? newValue,
    string existingValue)
    {
        return string.IsNullOrWhiteSpace(newValue)
            ? existingValue
            : newValue;
    }

    public static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }

            Console.WriteLine("Please enter a valid integer.");
        }
    }

    public static Gender ReadGender()
    {
        while (true)
        {
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");

            int choice = ReadInt("Select Gender:\t");

            switch (choice)
            {
                case 1:
                    return Gender.Male;

                case 2:
                    return Gender.Female;

                default:
                    PrintError("Invalid gender selection.");
                    break;
            }
        }
    }

    #endregion

    #region Console Output

    public static void PrintHeader(string title)
    {
        Console.Clear();

        Console.WriteLine(new string('=', 60));
        Console.WriteLine(title.ToUpper());
        Console.WriteLine(new string('=', 60));
        Console.WriteLine();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static bool Confirm(string message)
    {
        Console.Write($"{message} (Y/N):\t");

        string? input = Console.ReadLine();

        return string.Equals(input, "Y", StringComparison.OrdinalIgnoreCase);
    }

    public static void PrintInformation(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    #endregion

    #region Utility

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
    public static void LogIn()
    {
        string password;
        do
        {
            Console.Write("Please Enter a valid password:\t");
            password = Console.ReadLine();
            if (password != "Admin")
            {
                Console.WriteLine("Incorrect password, Please try again.\n");
            }
        }
        while (password != "Admin");
        Console.WriteLine("You have successfully loged into the SMS to perform desired actions.");
        Pause();
    }

    #endregion
}
