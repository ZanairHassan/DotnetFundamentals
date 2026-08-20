using System.ComponentModel.DataAnnotations;

namespace ASP.NetFundamentals.Models
{
    public class Developer
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        [Range(23, 45)]
        public int Age { get; set; }
        public string Role { get; set; } = string.Empty;
        public decimal Experience { get; set; }
        public decimal Salary { get; set; }
    }
}
