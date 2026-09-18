using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Authentication;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}