using System;
using System.Collections.Generic;
using System.Text;

namespace AdvanceLinqOperations.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public decimal Budget { get; set; }

        public string ManagerName { get; set; } = string.Empty;
    }
}
