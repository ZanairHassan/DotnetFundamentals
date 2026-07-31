using LinqOperations.Models;
using LinqOperations.Seed;
using LinqOperations.Services;
using System.ComponentModel.DataAnnotations;
StudentSeeder objSeed= new StudentSeeder();
List<Student> students= objSeed.SeedStudents();
StudentLinqService studentService=new StudentLinqService(students);
while (true)
{
    BusinessClass.Show();
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("========== WHERE ==========\n");
            Console.Write("Enter Departmet\t");
            string department= Console.ReadLine();
            Console.Write("Enter Age\t");
            int age = BusinessClass.ReadInt();
            foreach (var student in studentService.GetStudentsByDepartment(department,age))
            {
                Console.WriteLine($"{student.Name} | Age: {student.Age}");
            }
            break;
        case "2":
            Console.WriteLine("\n========== SELECT ==========\n");
            foreach (var name in studentService.GetStudentNames())
            {
                Console.WriteLine(name);
            }
            break;
        case "3":
            Console.WriteLine("\n========== ORDER BY ==========\n");
            foreach (var student in studentService.GetStudentsOrderedByMarks())
            {
                Console.WriteLine($"{student.Name} - {student.Marks}");
            }
            break;
        case "4":
            Console.WriteLine("\n========== FIRST OR DEFAULT ==========\n");

            var topper = studentService.GetTopper();
            if (topper != null)
            {
                Console.WriteLine($"{topper.Name} scored {topper.Marks}");
            }
            else
            {
                Console.WriteLine("No student found.");
            }
            break;
        case "5":
            Console.WriteLine("App Terminated");
            BusinessClass.Pause();
            return;
        default:
            Console.WriteLine("Invalid Option.");
            break;
    }
    BusinessClass.Pause();
}
