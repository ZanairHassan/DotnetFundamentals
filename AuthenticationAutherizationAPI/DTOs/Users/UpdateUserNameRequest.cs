using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Users;

public class UpdateUserNameRequest
{
    [Required]
    [StringLength(256, MinimumLength = 3)]
    public string UserName { get; set; } = string.Empty;
}
