using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.DTOs.Tenants;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class TenantService : ITenantService
{
    private readonly ApplicationDbContext _context;

    public TenantService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TenantResponse> CreateTenantAsync(CreateTenantRequest request)
    {
        var tenantKey = request.TenantKey.Trim();

        var exists = await _context.Tenants.AnyAsync(x => x.TenantKey == tenantKey);

        if (exists)
        {
            throw new InvalidOperationException("A tenant with this tenant key already exists.");
        }

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            TenantKey = tenantKey
        };

        _context.Tenants.Add(tenant);

        await _context.SaveChangesAsync();

        return new TenantResponse
        {
            Id = tenant.Id,
            Name = tenant.Name,
            TenantKey = tenant.TenantKey
        };
    }

    public async Task<TenantResponse?> GetTenantByKeyAsync(string tenantKey)
    {
        return await _context.Tenants
            .AsNoTracking()
            .Where(x => x.TenantKey == tenantKey)
            .Select(x => new TenantResponse
            {
                Id = x.Id,
                Name = x.Name,
                TenantKey = x.TenantKey
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TenantResponse>> GetAllTenantsAsync()
    {
        return await _context.Tenants
            .AsNoTracking()
            .Select(x => new TenantResponse
            {
                Id = x.Id,
                Name = x.Name,
                TenantKey = x.TenantKey
            })
            .ToListAsync();
    }
}