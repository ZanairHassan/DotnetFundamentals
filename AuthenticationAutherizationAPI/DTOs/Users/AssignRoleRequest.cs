using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Users;

public class AssignRoleRequest
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string RoleName { get; set; } = string.Empty;
}