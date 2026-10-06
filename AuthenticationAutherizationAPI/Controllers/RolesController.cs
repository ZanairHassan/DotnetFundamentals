using AuthenticationAutherizationAPI.DTOs.Roles;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Manager")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("getAllRoles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();

        var response = roles.Select(role => new RoleResponse
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        });

        return Ok(response);
    }
    [AllowAnonymous]
    [HttpPost("createRole")]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        var result = await _roleService.CreateRoleAsync(request.RoleName);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                Message = "Unable to create role.",
                Errors = result.Errors
            });
        }

        return Ok(new
        {
            Message = "Role created successfully."
        });
    }
}