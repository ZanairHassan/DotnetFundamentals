using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EncapsulationProperties
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public bool IsAvailable { get; set; }=false;
    }
}
