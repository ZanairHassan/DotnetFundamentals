using AuthenticationAutherizationAPI.DTOs.Tenants;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class TenantsController : ControllerBase
{
    private readonly ITenantService _tenantService;

    public TenantsController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [AllowAnonymous]
    [HttpGet("getTenants")]
    public async Task<IActionResult> GetAllTenants()
    {
        var tenants = await _tenantService.GetAllTenantsAsync();

        return Ok(tenants);
    }

    [HttpPost("createTenant")]
    public async Task<IActionResult> CreateTenant(CreateTenantRequest request)
    {
        var tenant = await _tenantService.CreateTenantAsync(request);

        return Ok(tenant);
    }

    [HttpGet("getTenant/{tenantKey}")]
    public async Task<IActionResult> GetTenantByKey(string tenantKey)
    {
        var tenant = await _tenantService.GetTenantByKeyAsync(tenantKey);

        if (tenant is null)
        {
            return NotFound($"Tenant with key '{tenantKey}' was not found.");
        }

        return Ok(tenant);
    }
}