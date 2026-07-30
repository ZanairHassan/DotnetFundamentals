using LinqOperations.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqOperations.Seed
{
    public class StudentSeeder
    {
        public  List<Student> SeedStudents()
        {
            Random random = new Random();

            string[] firstNames =
            {
                "Ali", "Ahmed", "Usman", "Hamza", "Bilal",
                "Ayesha", "Fatima", "Zainab", "Sara", "Hina",
                "Hassan", "Zain", "Danish", "Saad", "Talha",
                "Maryam", "Noor", "Iqra", "Maham", "Anum"
            };

            string[] lastNames =
            {
                "Khan", "Ahmed", "Malik", "Butt", "Raza",
                "Qureshi", "Sheikh", "Chaudhry", "Javed", "Mirza"
            };

            string[] departments =
            {
                "Computer Science",
                "Software Engineering",
                "Information Technology",
                "Artificial Intelligence",
                "Cyber Security",
                "English"
            };

            List<Student> students = new List<Student>();

            for (int i = 1; i <= 50; i++)
            {
                students.Add(new Student
                {
                    Id = i,
                    Name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}",
                    Age = random.Next(18, 31),
                    Department = departments[random.Next(departments.Length)],
                    Marks = Math.Round(random.NextDouble() * 50 + 50, 2) 
                });
            }

            return students;
        }
    }

}

