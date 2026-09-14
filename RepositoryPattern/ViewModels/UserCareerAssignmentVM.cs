using System.ComponentModel.DataAnnotations;

namespace RepositoryPattern.ViewModels
{
    public class UserCareerAssignmentVM
    {
        [Required]
        [MinLength(1, ErrorMessage = "Select at least one user.")]
        public List<int> SelectedUserIds { get; set; } = [];

        [Range(1, int.MaxValue, ErrorMessage = "Please select a career.")]
        [Display(Name = "Career")]
        public int CareerId { get; set; }
    }
}
