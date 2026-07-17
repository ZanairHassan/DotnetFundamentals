using System;
using System.Collections.Generic;
using System.Text;

namespace VariablesAndDataTypes
{
    public class VariablesAction
    {
        public void StudentDetails()
        {
            string name = "Zanair Hassan";
            int age = 25;
            string height = "5 feet 7 inches";
            string department = "Information Technology";
            double cgpa = 2.79;
            bool isGraduated = true;
            Console.WriteLine($"Student Name: {name} \nStudent Age: {age} \nStudent Height: {height} \nStudent Department: {department} \nStudent CGPA: {cgpa} \nStudent Is Graduated: {isGraduated}");
        }
        public void AreaOfRectangular(int length, int wirdth)
        {
            int area = length * wirdth;
            Console.WriteLine("Area = " + area);
        }
        public void SwapNumbers(int a, int b)
        {
            Console.WriteLine("Before swap\nFirst No:" + a + "\nSecond No: " + b);
            a = a + b;
            b = a - b;
            a = a - b;
            Console.WriteLine("After swap\nFirst No:" + a + "\nSecond No: " + b);
        }

        public void CircleAction(double radius)
        {
            const double pi = 3.14159;
            double diameterofCircle = 2 * radius;
            double circumferenceOfCircle = 2 * pi * radius;
            double areaOfCircle = pi * radius * radius;
            Console.WriteLine("The Diameter: " + diameterofCircle + "\nThe Circumference: " + circumferenceOfCircle + "\nThe Area: " + areaOfCircle);
        }

        public void LogicalComparision(int a, int b)
        {
            if (a == b)
            {
                Console.WriteLine("Both Inserted numbers are equal.");
            }
            if (a != b)
            {
                Console.WriteLine("Inserted numbers are not equal.");
                if (a > b)
                {
                    Console.WriteLine("The first inserted number is greater then the second number.");
                }
                if (a < b)
                {
                    Console.WriteLine("The first number is smaller then the second number.");
                }
                if (a > 20 && b < 7)
                {
                    Console.WriteLine("the first no is greater then 20 and the second number is smaller then 7");
                }
                if (a != 10 && b != 0)
                {
                    Console.WriteLine("The value of A is not 10 and the value of B is not 0");
                }
            }

        }

        public void VariableCasting()
        {
            int number = 102;
            double a = number;
            long b = number;
            float c = number;
            decimal d = number;
            Console.WriteLine("Integer: " + number + "\tType:\t" + number.GetType());
            Console.WriteLine("Double: " + a + "\tType:\t" + a.GetType());
            Console.WriteLine("Long: " + b + "\tType:\t" + b.GetType());
            Console.WriteLine("Float: " + c + "\tType:\t" + c.GetType());
            Console.WriteLine("Decimal: " + d + "\tType:\t" + d.GetType());
        }

    }
}
