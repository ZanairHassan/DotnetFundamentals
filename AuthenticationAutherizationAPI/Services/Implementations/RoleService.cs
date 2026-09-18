using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
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
}