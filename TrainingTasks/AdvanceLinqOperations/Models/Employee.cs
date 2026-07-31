using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLinqOperations.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string City { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public decimal Bonus { get; set; }

        public int Experience { get; set; }

        public DateTime JoiningDate { get; set; }

        public double PerformanceRating { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}
