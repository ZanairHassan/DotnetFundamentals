using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayCollection
{
    public class CollectionsActions
    {
        public List<Student> actions = new List<Student>();
        public void AddStudent()
        {
            Student student = new Student();
            Console.Write("Enter Roll No:\t");
            student.RollNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Name:\t");
            student.Name = Console.ReadLine();
            Console.Write("Enter Age:\t");
            student.Age= Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Marks:\t");
            student.Marks= Convert.ToDouble(Console.ReadLine());
            actions.Add(student);
            Console.WriteLine("\nStudent Added Successfully.");
        }

        public void DisplayStudent()
        {
            if (actions.Count == 0)
            {
                Console.WriteLine("No Students Found.");
                return;
            }
            foreach (Student student in actions)
            {
                Console.WriteLine("********************");
                Console.WriteLine($"Student Name:\t{student.Name}");
                Console.WriteLine($"Student Roll No:\t{student.RollNo}");
                Console.WriteLine($"Student Age:\t{student.Age}");
                Console.WriteLine($"Student Marks:\t{student.Marks}");
            }
        }
        public void SearchStudent()
        {
            Console.Write("Enter Roll No:\t");
            int rollNo= Convert.ToInt32(Console.ReadLine());
            if(actions.Count == 0)
            {
                Console.WriteLine("No Students Found.");
                return;
            }
            foreach(Student student in actions)
            {
                if(student.RollNo == rollNo)
                {
                    Console.WriteLine("\nStudent Found");
                    Console.WriteLine($"Name : {student.Name}");
                    Console.WriteLine($"Age : {student.Age}");
                    Console.WriteLine($"Marks : {student.Marks}");
                    return;
                }
            }
        }

        public void UpdateStudent()
        {
            Console.Write("Enter Roll No:\t");
            int rollNo=Convert.ToInt32(Console.ReadLine());
            foreach(Student student in actions)
            {
                if(student.RollNo == rollNo)
                {
                    Console.Write("New Name:\t");
                    student.Name = Console.ReadLine();

                    student.RollNo = rollNo;

                    Console.Write("New Age:\t");
                    student.Age = Convert.ToInt32(Console.ReadLine());

                    Console.Write("New Marks:\t");
                    student.Marks = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Student Updated.");
                    return;
                }
            }
            Console.WriteLine("No Students Found.");
        }

        public void DeleteStudent()
        {
            Console.Write("Enter Roll No : ");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            Student deleteStudent = null;

            foreach (Student student in actions)
            {
                if (student.RollNo == rollNo)
                {
                    deleteStudent = student;
                    break;
                }
            }

            if (deleteStudent != null)
            {
                actions.Remove(deleteStudent);
                Console.WriteLine("Student Deleted.");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }
    }
}
