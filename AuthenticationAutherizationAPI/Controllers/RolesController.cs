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

    [HttpPut("updateRole/{roleId}")]
    public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateRoleAsync(roleId, request.NewRoleName);

        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code == "RoleNotFound"))
            {
                return NotFound(new
                {
                    Message = "Role was not found."
                });
            }

            return BadRequest(new
            {
                Message = "Unable to update role.",
                Errors = result.Errors
            });
        }

        return Ok(new
        {
            Message = "Role updated successfully."
        });
    }

    [HttpDelete("deleteRole/{roleId}")]
    public async Task<IActionResult> DeleteRole(string roleId)
    {
        var result = await _roleService.DeleteRoleAsync(roleId);

        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code == "RoleNotFound"))
            {
                return NotFound(new
                {
                    Message = "Role was not found."
                });
            }

            return BadRequest(new
            {
                Message = "Unable to delete role.",
                Errors = result.Errors
            });
        }

        return Ok(new
        {
            Message = "Role deleted successfully."
        });
    }

    [HttpGet("getAssignedRoles")]
    public async Task<IActionResult> GetAssignedRoles()
    {
        var roles = await _roleService.GetAssignedRolesAsync();

        return Ok(roles);
    }

    [HttpGet("getAssignedUserCount/{roleId}")]
    public async Task<IActionResult> GetAssignedUserCount(string roleId)
    {
        var count = await _roleService.GetAssignedUserCountAsync(roleId);

        return Ok(new
        {
            RoleId = roleId,
            AssignedUsersCount = count
        });
    }

    [HttpGet("isNormalRoleAssigned/{roleId}")]
    public async Task<IActionResult> IsNormalRoleAssigned(string roleId)
    {
        var isAssigned = await _roleService.IsRoleAssignedToAnyUserAsync(roleId);

        return Ok(new
        {
            RoleId = roleId,
            IsAssigned = isAssigned
        });
    }

    [HttpGet("getNormalAssignedUserCount/{roleId}")]
    public async Task<IActionResult> GetNormalAssignedUserCount(string roleId)
    {
        var count = await _roleService.GetAssignedUserCountAsync(roleId);

        return Ok(new
        {
            RoleId = roleId,
            AssignedUsersCount = count
        });
    }

    [HttpGet("isRoleAssigned/{roleId}")]
    public async Task<IActionResult> IsRoleAssigned(string roleId)
    {
        var isAssigned = await _roleService.IsRoleAssignedToAnyUserAsync(roleId);

        return Ok(new
        {
            RoleId = roleId,
            IsAssigned = isAssigned
        });
    }
}