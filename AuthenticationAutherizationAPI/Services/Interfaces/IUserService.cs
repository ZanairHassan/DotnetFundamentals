using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IUserService
{
    Task<IList<ApplicationUser>> GetAllUsersAsync();

    Task<ApplicationUser?> GetUserByIdAsync(string userId);

    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

    Task<IdentityResult> AddToRoleAsync(string userId, string roleName);

    Task<IdentityResult> ReplaceRoleAsync(string userId, string currentRoleName, string newRoleName);

    Task<IdentityResult> RemoveFromRoleAsync(string userId, string roleName);

    Task<IdentityResult> UpdateUserNameAsync(string userId, string userName);

    Task<IdentityResult> DeleteUserAsync(string userId);

    Task<IList<string>> GetRolesAsync(string userId);

    Task<IdentityResult> SetLockoutEnabledAsync(string userId, bool enabled);

    Task<bool> GetLockoutEnabledAsync(string userId);
}