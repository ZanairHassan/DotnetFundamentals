using AuthenticationAutherizationAPI.DTOs.Users;
using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserResponse>> GetAllUsersAsync();

    Task<UserResponse?> GetUserByIdAsync(string userId);

    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, string tenantKey);

    Task<IdentityResult> AddToRoleAsync(string userId, string roleName);

    Task<IdentityResult> ReplaceRoleAsync(string userId, string currentRoleName, string newRoleName);

    Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName);

    Task<IdentityResult> UpdateUserNameAsync(string userId, string userName);

    Task<IdentityResult> DeleteUserAsync(string userId);

    Task<IList<string>> GetRolesAsync(string userId);

    Task<IdentityResult> SetLockoutEnabledAsync(string userId, bool enabled);

    Task<bool?> GetLockoutEnabledAsync(string userId);

    Task<bool> AssignTenantAsync(string userId, string tenantKey);
}