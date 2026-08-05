using System;
using System.Collections.Generic;
using System.Text;

namespace LinqOperations.Models
{
    public class Student 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Department { get; set; } = string.Empty;
        public double Marks { get; set; } 
    }
}
