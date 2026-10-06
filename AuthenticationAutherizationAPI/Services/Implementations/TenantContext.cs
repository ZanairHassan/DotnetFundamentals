using AuthenticationAutherizationAPI.Services.Interfaces;
using System.Security.Claims;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? CurrentTenantId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var tenantIdClaim = user?.FindFirst("tenant_id")?.Value
                ?? user?.FindFirst("TenantId")?.Value
                ?? _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].FirstOrDefault()
                ?? _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-ID"].FirstOrDefault();

            if (Guid.TryParse(tenantIdClaim, out var tenantId))
            {
                return tenantId;
            }

            return null;
        }
    }

    public Guid TenantId => CurrentTenantId ?? throw new InvalidOperationException("Tenant ID is missing or invalid.");
}