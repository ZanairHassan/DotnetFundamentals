using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Authentication;

public class LoginRequest
{
    [Required]
    public string TenantKey { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}