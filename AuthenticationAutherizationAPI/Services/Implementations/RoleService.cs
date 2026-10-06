using AuthenticationAutherizationAPI.DTOs.Roles;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly CustomRoleManager _roleManager;

    public RoleService(CustomRoleManager roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IList<ApplicationRole>> GetAllRolesAsync()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IdentityResult> CreateRoleAsync(string roleName)
    {
        roleName = roleName.Trim();

        if (string.IsNullOrWhiteSpace(roleName))
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "InvalidRoleName",
                    Description = "Role name cannot be empty."
                });
        }

        var roleExists = await _roleManager.RoleExistsAsync(roleName);

        if (roleExists)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "RoleAlreadyExists",
                    Description = "Role already exists."
                });
        }

        var role = new ApplicationRole
        {
            Name = roleName
        };

        return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName)
    {
        return await _roleManager.UpdateRoleNameAsync(roleId, newRoleName);
    }

    public async Task<IdentityResult> DeleteRoleAsync(string roleId)
    {
        return await _roleManager.DeleteRoleByIdAsync(roleId);
    }

    public async Task<IList<AssignedRoleResponse>> GetAssignedRolesAsync()
    {
        var rolesWithCount = await _roleManager.GetRolesWithAssignedUserCountAsync();

        return rolesWithCount.Select(x => new AssignedRoleResponse
        {
            Id = x.Role.Id,
            Name = x.Role.Name ?? string.Empty,
            AssignedUsersCount = x.UserCount
        }).ToList();
    }
}