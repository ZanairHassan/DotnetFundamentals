using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class CustomRoleManager : RoleManager<ApplicationRole>
{
    private readonly ILogger<CustomRoleManager> _customLogger;
    private readonly ApplicationDbContext _dbContext;

    #region

    private static readonly Func<ApplicationDbContext, string, Task<int>>
    GetAssignedUserCountCompiled =
        EF.CompileAsyncQuery(
            (ApplicationDbContext context, string roleId) =>
                context.UserRoles.Count(ur => ur.RoleId == roleId));

    private static readonly Func<ApplicationDbContext, string, Task<bool>>
        IsRoleAssignedToAnyUserCompiled =
            EF.CompileAsyncQuery(
                (ApplicationDbContext context, string roleId) =>
                    context.UserRoles.Any(ur => ur.RoleId == roleId));

    #endregion
    public CustomRoleManager(
        IRoleStore<ApplicationRole> store,
        IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager<ApplicationRole>> logger,
        ILogger<CustomRoleManager> customLogger,
        ApplicationDbContext dbContext)
        : base(store, roleValidators, keyNormalizer, errors, logger)
    {
        _customLogger = customLogger;
        _dbContext = dbContext;
    }

    public override async Task<IdentityResult> DeleteAsync(ApplicationRole role)
    {
        ArgumentNullException.ThrowIfNull(role);

        _customLogger.LogInformation("Processing deletion request for role '{RoleName}' (Id: {RoleId}).", role.Name, role.Id);

        var userRoles = await _dbContext.UserRoles.Where(ur => ur.RoleId == role.Id).ToListAsync();

        if (userRoles.Count > 0)
        {
            _customLogger.LogInformation(
                "Role '{RoleName}' (Id: {RoleId}) is currently assigned to {Count} user(s). Removing associations from [dbo].[AspNetUserRoles] before deleting role.",
                role.Name, role.Id, userRoles.Count);

            _dbContext.UserRoles.RemoveRange(userRoles);
            await _dbContext.SaveChangesAsync();

            _customLogger.LogInformation(
                "Successfully removed {Count} user association(s) for role '{RoleName}' from [dbo].[AspNetUserRoles].",
                userRoles.Count, role.Name);
        }

        var result = await base.DeleteAsync(role);

        if (result.Succeeded)
        {
            _customLogger.LogInformation("Role '{RoleName}' (Id: {RoleId}) deleted successfully.", role.Name, role.Id);
        }
        else
        {
            _customLogger.LogWarning("Failed to delete role '{RoleName}' (Id: {RoleId}). Errors: {Errors}",
                role.Name, role.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return result;
    }

    public async Task<IdentityResult> DeleteRoleByIdAsync(string roleIdentifier)
    {
        if (string.IsNullOrWhiteSpace(roleIdentifier))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidRoleId",
                Description = "Role identifier cannot be empty."
            });
        }

        var role = await FindByIdAsync(roleIdentifier) ?? await FindByNameAsync(roleIdentifier);
        if (role is null)
        {
            _customLogger.LogWarning("Delete role failed: Role '{RoleIdentifier}' not found.", roleIdentifier);
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleIdentifier}' was not found."
            });
        }

        return await DeleteAsync(role);
    }

    public override async Task<IdentityResult> UpdateAsync(ApplicationRole role)
    {
        ArgumentNullException.ThrowIfNull(role);

        _customLogger.LogInformation("Updating role '{RoleName}' (Id: {RoleId}).", role.Name, role.Id);

        await UpdateNormalizedRoleNameAsync(role);

        var result = await base.UpdateAsync(role);

        if (result.Succeeded)
        {
            _customLogger.LogInformation("Role '{RoleName}' (Id: {RoleId}) updated successfully.", role.Name, role.Id);
        }
        else
        {
            _customLogger.LogWarning(
                "Failed to update role '{RoleName}' (Id: {RoleId}). Errors: {Errors}",
                role.Name, role.Id, string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        return result;
    }

    public async Task<IdentityResult> UpdateRoleNameAsync(string roleIdentifier, string newRoleName)
    {
        newRoleName = newRoleName.Trim();

        if (string.IsNullOrWhiteSpace(newRoleName))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidRoleName",
                Description = "Role name cannot be empty."
            });
        }

        var role = await FindByIdAsync(roleIdentifier) ?? await FindByNameAsync(roleIdentifier);
        if (role is null)
        {
            _customLogger.LogWarning("Update role failed: Role '{RoleIdentifier}' not found.", roleIdentifier);
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = $"Role '{roleIdentifier}' was not found."
            });
        }

        var existingRoleWithNewName = await FindByNameAsync(newRoleName);
        if (existingRoleWithNewName is not null && existingRoleWithNewName.Id != role.Id)
        {
            _customLogger.LogWarning("Update role failed: Role name '{NewRoleName}' already exists.", newRoleName);
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleAlreadyExists",
                Description = $"Role '{newRoleName}' already exists."
            });
        }

        role.Name = newRoleName;
        return await UpdateAsync(role);
    }

    public async Task<IList<ApplicationRole>> GetRolesAssignedToUsersAsync()
    {
        _customLogger.LogInformation("Querying all roles currently assigned to users.");

        var assignedRoleIds = await _dbContext.UserRoles
            .Select(ur => ur.RoleId)
            .Distinct()
            .ToListAsync();

        return await Roles.Where(r => assignedRoleIds.Contains(r.Id)).ToListAsync();
    }

    public async Task<IList<(ApplicationRole Role, int UserCount)>> GetRolesWithAssignedUserCountAsync()
    {
        _customLogger.LogInformation("Querying all assigned roles with user counts.");

        var countsByRoleId = await _dbContext.UserRoles
            .GroupBy(ur => ur.RoleId)
            .Select(g => new { RoleId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.RoleId, x => x.Count);

        var assignedRoleIds = countsByRoleId.Keys.ToList();

        var roles = await Roles.Where(r => assignedRoleIds.Contains(r.Id)).ToListAsync();

        return roles.Select(role => (Role: role, UserCount: countsByRoleId[role.Id])).ToList();
    }

    public async Task<bool> IsRoleAssignedToAnyUserAsync(string roleId)
    {
        return await _dbContext.UserRoles.AnyAsync(ur => ur.RoleId == roleId);
    }

    public async Task<int> GetAssignedUserCountAsync(string roleId)
    {
        return await _dbContext.UserRoles.CountAsync(ur => ur.RoleId == roleId);
    }

    public async Task<int> GetAssignedUserCountCompiledAsync(string roleId)
    {
        return await GetAssignedUserCountCompiled(_dbContext, roleId);
    }

    public async Task<bool> IsRoleAssignedToAnyUserCompiledAsync(string roleId)
    {
        return await IsRoleAssignedToAnyUserCompiled(_dbContext, roleId);
    }
}
