namespace AuthenticationAutherizationAPI.DTOs.Tenants;

public class TenantResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string TenantKey { get; set; } = string.Empty;
}