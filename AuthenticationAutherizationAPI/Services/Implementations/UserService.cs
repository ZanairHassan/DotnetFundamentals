using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IList<ApplicationUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User was not found."
                });
        }

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            return RoleNotFound(roleName);
        }

        if (await _userManager.IsInRoleAsync(user, roleName))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserAlreadyInRole",
                Description = "The user is already assigned to this role."
            });
        }

        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> ReplaceRoleAsync(string userId, string currentRoleName, string newRoleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserNotFound();
        }

        if (string.Equals(currentRoleName, newRoleName, StringComparison.OrdinalIgnoreCase))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RolesMustDiffer",
                Description = "The current and new role must be different."
            });
        }

        if (!await _userManager.IsInRoleAsync(user, currentRoleName))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "CurrentRoleNotAssigned",
                Description = "The user is not assigned to the current role."
            });
        }

        if (!await _roleManager.RoleExistsAsync(newRoleName))
        {
            return RoleNotFound(newRoleName);
        }

        var addResult = await _userManager.AddToRoleAsync(user, newRoleName);

        if (!addResult.Succeeded)
        {
            return addResult;
        }

        return await _userManager.RemoveFromRoleAsync(user, currentRoleName);
    }

    public async Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return UserNotFound();
        }

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            return RoleNotFound(roleName);
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotAssigned",
                Description = "The user is not assigned to this role."
            });
        }

        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> UpdateUserNameAsync(string userId, string userName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user is null ? UserNotFound() : await _userManager.SetUserNameAsync(user, userName);
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user is null ? UserNotFound() : await _userManager.DeleteAsync(user);
    }

    public async Task<IList<string>> GetRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return [];
        }

        return await _userManager.GetRolesAsync(user);
    }

    private static IdentityResult UserNotFound()
    {
        return IdentityResult.Failed(new IdentityError
        {
            Code = "UserNotFound",
            Description = "User was not found."
        });
    }

    private static IdentityResult RoleNotFound(string roleName)
    {
        return IdentityResult.Failed(new IdentityError
        {
            Code = "RoleNotFound",
            Description = $"The role '{roleName}' was not found."
        });
    }
}
