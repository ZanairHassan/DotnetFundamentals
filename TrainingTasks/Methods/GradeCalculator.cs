using System;
using System.Collections.Generic;
using System.Text;

namespace Methods
{
    public class GradeCalculator
    {
        public int[] GetMarks()
        {
            int[] marks = new int[5];

            for (int i = 0; i < marks.Length; i++)
            {
                while (true)
                {
                    Console.Write($"Enter Marks of Subject {i + 1}:\t");
                    int enteredMarks = Convert.ToInt32(Console.ReadLine());
                    if (enteredMarks >= 0 && enteredMarks <= 100)
                    {
                        marks[i] = enteredMarks;
                        break;
                    }
                    else
                        Console.WriteLine("Invalid marks! Please enter marks between 0 and 100.\n");
                }
            }

            return marks;
        }

        public double CalculateTotal(int[] marks)
        {
            int total = 0;

            foreach (int mark in marks)
            {
                total += mark;
            }
            double totalMarks = total;
            return totalMarks;
        }

        public double CalculatePercentage(double total)
        {
            return (total /500)*100;
        }

        public string GetGrade(double percentage)
        {
            if (percentage >= 85)
                return "A+";
            else if (percentage >= 80)
                return "A";
            else if (percentage >= 75)
                return "B+";
            else if (percentage >= 70)
                return "B";
            else if (percentage >= 65)
                return "B-";
            else if (percentage >= 60)
                return "C+";
            else if (percentage >= 55)
                return "C";
            else if (percentage >= 50)
                return "D";

            else
                return "F";
        }

        public void DisplayResult(string name, double total, double percentage, string grade)
        {
            Console.WriteLine("\n************* RESULT *******************");

            Console.WriteLine($"Student Name :\t{name}");
            Console.WriteLine($"Total Marks  :\t{total}/500");
            Console.WriteLine($"Percentage   :\t{percentage:F2}%");
            Console.WriteLine($"Grade        :\t{grade}");
        }
    }
}

