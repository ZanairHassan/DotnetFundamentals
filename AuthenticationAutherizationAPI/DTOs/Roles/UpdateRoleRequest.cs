using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Roles;

public class UpdateRoleRequest
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NewRoleName { get; set; } = string.Empty;
}
