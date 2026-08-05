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
        public StudentLinqService(List<Student> students)
        {
            _students = students;
        }
        
        public IEnumerable<Student> GetStudentsByDepartment(string department, int age)
        {
            var result = _students.Where(s => s.Age > age && s.Department == department);
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
