using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IUserService
{
    Task<IList<ApplicationUser>> GetAllUsersAsync();

    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

    Task<IdentityResult> AddToRoleAsync(string userId, string roleName);

    Task<IList<string>> GetRolesAsync(string userId);
}