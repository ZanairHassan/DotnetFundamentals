using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace EncapsulationProperties
{
    public class EmployeeService
    {
        private readonly List<Employee> _employees;
        public EmployeeService()
        {
            _employees = new List<Employee>();
        }
        private void DisplayEmployee(Employee employee)
        {
            Console.WriteLine("**********The Employees details **************");
            Console.WriteLine($"ID          : {employee.ID}");
            Console.WriteLine($"Name        : {employee.Name}");
            Console.WriteLine($"Email       : {employee.Email}");
            Console.WriteLine($"Age         : {employee.Age}");
            Console.WriteLine($"Salary      : {employee.Salary}");
            Console.WriteLine($"Department  : {employee.Department}");
            Console.WriteLine($"Designation : {employee.Designation}");
            Console.WriteLine($"Available   : {employee.IsAvailable}");
            Console.WriteLine("********************************");
        }
        private void EmployeeProperties(Employee employee)
        {
            Console.Write("Enter Name:\t");
            employee.Name = ReadUserName();
            Console.Write("Enter Email:\t");
            employee.Email = ReadEmail();
            Console.Write("Enter Age:\t");
            employee.Age = ReadInt();
            Console.Write("Enter Salary:\t");
            employee.Salary = ReadDouble();
            Console.Write("Enter Department:\t");
            employee.Department = Console.ReadLine();
            Console.Write("Enter Designation:\t");
            employee.Designation = Console.ReadLine();
            Console.Write("Enter IsAvailable:\t");
            employee.IsAvailable = ReadBoolean();
        }
        public void AddEmployee()
        {
            try
            {
                Employee employee = new Employee();

                Console.Write("Enter ID:\t");
                employee.ID = ReadInt();
                while (_employees.Exists(e => e.ID == employee.ID))
                {
                    Console.WriteLine("Employee ID already exists.");
                    Console.Write("Please enter a different ID:\t");
                    employee.ID = ReadInt();
                }
                EmployeeProperties(employee);
                _employees.Add(employee);
                Console.WriteLine("Employee added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void GetEmployeeByID(int id)
        {
            try
            {
                Employee objEmployee=_employees.Find(e=> e.ID == id);
                if(objEmployee == null)
                {
                    Console.WriteLine("Employee against the entered ID has not found");
                    return;
                }
                DisplayEmployee(objEmployee);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllEmployees()
        {
            try
            {
                if (_employees.Count == 0)
                {
                    Console.WriteLine("No employee found.");
                    return;
                }
                foreach(Employee emp in _employees)
                {
                    DisplayEmployee(emp);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateEmployee(int ID)
        {
            try
            {
                Employee employee = _employees.Find(e => e.ID == ID);

                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                    return;
                }
                EmployeeProperties(employee);
                Console.WriteLine("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                Employee employee = _employees.Find(e => e.ID == id);

                if (employee == null)
                {
                    Console.WriteLine("Employee not found.");
                    return;
                }

                _employees.Remove(employee);

                Console.WriteLine("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool ReadBoolean()
        {
            bool result;
            while (!bool.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid Case, try again:\t");
            return result;
        }
        public int ReadInt()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid number, try again:\t");
            return result;
        }
        public double ReadDouble()
        {
            double result;
            while (!double.TryParse(Console.ReadLine(), out result))
                Console.Write("Invalid number, try again:\t");
            return result;
        }

        public string ReadUserName()
        {
            while (true)
            {
                string? username = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(username))
                    return username;

                Console.WriteLine("Username cannot be empty.\n");
            }
        }

        public string ReadEmail()
        {
            while (true)
            {
                string? email = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("Email cannot be empty.\n");
                    continue;
                }

                try
                {
                    MailAddress mail = new MailAddress(email);

                    if (mail.Address == email)
                        return email;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("Invalid email address.\n");
            }
        }
    }
}
