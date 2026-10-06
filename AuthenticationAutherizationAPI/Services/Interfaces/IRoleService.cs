using AuthenticationAutherizationAPI.DTOs.Roles;
using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(string roleName);

    Task<IList<ApplicationRole>> GetAllRolesAsync();

    Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName);

    Task<IdentityResult> DeleteRoleAsync(string roleId);

    Task<IList<AssignedRoleResponse>> GetAssignedRolesAsync();

    Task<int> GetAssignedUserCountAsync(string roleId);

    Task<bool> IsRoleAssignedToAnyUserAsync(string roleId);

    Task<int> GetNormalAssignedUserCountAsync(string roleId);

    Task<bool> IsRoleNormalAssignedToAnyUserAsync(string roleId);
}