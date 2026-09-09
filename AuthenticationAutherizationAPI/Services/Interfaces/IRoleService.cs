using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(string roleName);

    Task<IList<ApplicationRole>> GetAllRolesAsync();
}