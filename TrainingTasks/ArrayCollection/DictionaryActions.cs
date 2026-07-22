using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayCollection
{
    public class DictionaryActions
    {
        public Dictionary<int, Student>students=new Dictionary<int, Student>();

        public void AddStudent()
        {
            Student student = new Student();

            Console.Write("Enter Roll No\t");
            student.RollNo = Convert.ToInt32(Console.ReadLine());

            if (students.ContainsKey(student.RollNo))
            {
                Console.WriteLine("This Roll Number Already Exists.");
                return;
            }

            Console.Write("Enter Name:\t");
            student.Name = Console.ReadLine();

            Console.Write("Enter Age:\t");
            student.Age = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Marks:\t");
            student.Marks = Convert.ToDouble(Console.ReadLine());

            students.Add(student.RollNo, student);

            Console.WriteLine("\nStudent Added Successfully.");
        }

        public void DisplayStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No Students Found.");
                return;
            }

            foreach (KeyValuePair<int, Student> item in students)
            {
                Console.WriteLine("*******************************");
                Console.WriteLine($"Roll No :\t{item.Key}");
                Console.WriteLine($"Name    :\t{item.Value.Name}");
                Console.WriteLine($"Age     :\t{item.Value.Age}");
                Console.WriteLine($"Marks   :\t{item.Value.Marks}");
            }
        }

        public void SearchStudent()
        {
            Console.Write("Enter Roll No:\t");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            if (students.ContainsKey(rollNo))
            {
                Student student = students[rollNo];

                Console.WriteLine("\nStudent Found");
                Console.WriteLine($"Name  :\t{student.Name}");
                Console.WriteLine($"Age   :\t{student.Age}");
                Console.WriteLine($"Marks :\t{student.Marks}");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }

        public void UpdateStudent()
        {
            Console.Write("Enter Roll No :\t");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            if (students.ContainsKey(rollNo))
            {
                Student student = students[rollNo];

                Console.Write("Enter New Name :\t");
                student.Name = Console.ReadLine();

                student.RollNo= rollNo;
                
                Console.Write("Enter New Age :\t");
                student.Age = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter New Marks :\t");
                student.Marks = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Student Updated Successfully.");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }

        public void DeleteStudent()
        {
            Console.Write("Enter Roll No :\t");
            int rollNo = Convert.ToInt32(Console.ReadLine());

            if (students.Remove(rollNo))
            {
                Console.WriteLine("Student Deleted Successfully.");
            }
            else
            {
                Console.WriteLine("Student Not Found.");
            }
        }
        public void DisplayKeys()
        {
            Console.WriteLine("\nRoll Numbers");

            foreach (int key in students.Keys)
            {
                Console.WriteLine(key);
            }
        }
        public void DisplayStudentNames()
        {
            Console.WriteLine("\nStudent Names");

            foreach (Student student in students.Values)
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}
