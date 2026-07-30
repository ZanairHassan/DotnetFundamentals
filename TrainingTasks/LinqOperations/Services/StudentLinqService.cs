using LinqOperations.Models;
using LinqOperations.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqOperations.Services
{
    public class StudentLinqService
    {
        private readonly List<Student> _students;
        public StudentLinqService(List<Student> studentSeeder)
        {
            _students = studentSeeder;
        }
        public void Pause()
        {
            Console.WriteLine();

            Console.Write("Press any key...");
            Console.ReadKey();
        }

        public void Show()
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
        public IEnumerable<Student> GetStudentsAgeGreaterThan24()
        {
            var result = _students.Where(s => s.Age > 24 && s.Department == "Information Technology");
            return result;
        }

        public IEnumerable<string> GetStudentNames()
        {
            return _students
                .Where(s=> s.Age<22)
                .Select(s => s.Name);
        }

        public IEnumerable<Student> GetStudentsOrderedByMarks()
        {
            return _students.OrderBy(s => s.Marks);
        }

        public Student? GetTopper()
        {
            var student = _students.OrderByDescending(s => s.Marks).FirstOrDefault();
            return student;
        }
    }
}
