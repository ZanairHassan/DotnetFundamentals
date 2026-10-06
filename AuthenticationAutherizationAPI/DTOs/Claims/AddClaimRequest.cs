using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Claims;

public class AddClaimRequest
{
    [Required]
    [StringLength(50)]
    public string ClaimType { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ClaimValue { get; set; } = string.Empty;
}