using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.DTOs.Users;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IReadOnlyList<UserResponse>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        var query = _userManager.Users
            .AsNoTracking()
            .OrderBy(user => user.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled
            });

        Console.WriteLine(query.ToQueryString());

        return await query.ToListAsync();
    }

    public async Task<UserResponse?> GetUserByIdAsync(string userId)
    {
        return await _userManager.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .Select(user => new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled
            })
            .SingleOrDefaultAsync();
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user,string password, string tenantKey)
    {
        var normalizedTenantKey = tenantKey.Trim();

        var tenantId = await _context.Tenants
            .AsNoTracking()
            .Where(tenant => tenant.TenantKey == normalizedTenantKey)
            .Select(tenant => (Guid?)tenant.Id)
            .SingleOrDefaultAsync();

        if (tenantId is null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "TenantNotFound",
                Description = "The specified tenant does not exist."
            });
        }

        user.TenantId = tenantId.Value;

        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> AddToRoleAsync(
        string userId,
        string roleName)
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

    public async Task<IdentityResult> SetLockoutEnabledAsync(string userId, bool enabled)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user is null ? UserNotFound() : await _userManager.SetLockoutEnabledAsync(user, enabled);
    }

    public async Task<bool?> GetLockoutEnabledAsync(string userId)
    {
        return await _userManager.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .Select(user => (bool?)user.LockoutEnabled)
            .SingleOrDefaultAsync();
    }

    public async Task<bool> AssignTenantAsync(string userId, string tenantKey)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            var normalizedTenantKey = tenantKey.Trim();

            var tenantId = await _context.Tenants
                .AsNoTracking()
                .Where(tenant => tenant.TenantKey == normalizedTenantKey)
                .Select(tenant => (Guid?)tenant.Id)
                .SingleOrDefaultAsync();

            if (tenantId is null)
            {
                await transaction.RollbackAsync();
                return false;
            }

            user.TenantId = tenantId.Value;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                await transaction.RollbackAsync();
                return false;
            }

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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