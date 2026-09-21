using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Users;

public class ReplaceUserRoleRequest
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string CurrentRoleName { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string NewRoleName { get; set; } = string.Empty;
}
