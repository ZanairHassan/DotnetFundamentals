using StudentManagementSystem.Enums;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.DataSeeding;

public static class StudentSeed
{
    public static List<Student> Seed()
    {
        Random random = Random.Shared;

        string[] firstNames =
        {
            "Ali","Ahmed","Usman","Hamza","Bilal",
            "Hassan","Zain","Saad","Talha","Danish",
            "Ayesha","Fatima","Zainab","Sara","Hina",
            "Noor","Iqra","Maham","Anum","Maryam"
        };

        string[] lastNames =
        {
            "Khan","Malik","Butt","Qureshi","Ahmed",
            "Raza","Chaudhry","Sheikh","Javed","Mirza"
        };

        Gender gender = random.Next(2) == 0
               ? Gender.Male
               : Gender.Female;

        List<Student> students = new();

        for (int i = 1; i <= 50; i++)
        {
            string firstName = firstNames[random.Next(firstNames.Length)];
            string lastName = lastNames[random.Next(lastNames.Length)];

            students.Add(new Student
            {
                Id = i,
                RegistrationNumber = $"STD-{i:0000}",
                FirstName = firstName,
                LastName = lastName,
                Age = random.Next(18, 28),
                Gender = gender,
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@gmail.com",
                PhoneNumber = $"03{random.Next(10, 50)}-{random.Next(1000000, 9999999)}",
                DepartmentId = random.Next(1, 6),
                EnrollmentDate = DateTime.Today.AddDays(-random.Next(100, 1200)),
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
        }

        return students;
    }
}
