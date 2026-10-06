using System.ComponentModel.DataAnnotations;

namespace AuthenticationAutherizationAPI.DTOs.Tenants;

public class CreateTenantRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string TenantKey { get; set; } = string.Empty;
}