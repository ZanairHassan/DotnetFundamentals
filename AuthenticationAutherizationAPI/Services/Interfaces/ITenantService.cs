using AuthenticationAutherizationAPI.DTOs.Tenants;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ITenantService
{
    Task<TenantResponse> CreateTenantAsync(CreateTenantRequest request);

    Task<TenantResponse?> GetTenantByKeyAsync(string tenantKey); 
    Task<IEnumerable<TenantResponse>> GetAllTenantsAsync();
}