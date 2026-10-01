namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ITenantContext
{
    Guid TenantId { get; }
    Guid? CurrentTenantId { get; }
}