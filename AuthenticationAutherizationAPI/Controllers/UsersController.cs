using AuthenticationAutherizationAPI.DTOs.Users;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        var response = users.Select(user => new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            EmailConfirmed = user.EmailConfirmed
        });

        return Ok(response);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user is null)
        {
            return NotFound(new { Message = "User was not found." });
        }

        return Ok(new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            EmailConfirmed = user.EmailConfirmed
        });
    }

    [AllowAnonymous]
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName.Trim(),
            Email = request.Email.Trim(),
            TwoFactorEnabled = request.TwoFactorEnabled
        };

        var result = await _userService.CreateUserAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new
        {
            Message = "User created successfully."
        });
    }

    [AllowAnonymous]
    [HttpPost("assignRoles/{userId}")]
    public async Task<IActionResult> AssignRole(string userId, AssignRoleRequest request)   
    {
        var result = await _userService.AddToRoleAsync(userId, request.RoleName.Trim());

        if (!result.Succeeded)
        {
            if (result.Errors.Any(error => error.Code == "UserNotFound"))
            {
                return NotFound(new
                {
                    Message = "User was not found."
                });
            }

            return BadRequest(new
            {
                Message = "Unable to assign role.",
                Errors = result.Errors
            });
        }

        return Ok(new
        {
            Message = "Role assigned successfully."
        });
    }

    [HttpPut("{userId}/userName")]
    public async Task<IActionResult> UpdateUserName(string userId, UpdateUserNameRequest request)
    {
        var result = await _userService.UpdateUserNameAsync(userId, request.UserName.Trim());

        return ToUserManagementResult(result, "Username updated successfully.");
    }

    [HttpPut("{userId}/roles")]
    public async Task<IActionResult> ReplaceRole(string userId, ReplaceUserRoleRequest request)
    {
        var result = await _userService.ReplaceRoleAsync(
            userId,
            request.CurrentRoleName.Trim(),
            request.NewRoleName.Trim());

        return ToUserManagementResult(result, "Role updated successfully.");
    }

    [HttpDelete("{userId}/roles/{roleName}")]
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
    {
        var result = await _userService.RemoveFromRoleAsync(userId, roleName);

        return ToUserManagementResult(result, "Role removed successfully.");
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await _userService.DeleteUserAsync(userId);

        return ToUserManagementResult(result, "User deleted successfully.");
    }

    [HttpGet("getAssignedRoles/{userId}")]
    public async Task<IActionResult> GetRoles(string userId)
    {
        try
        {
            var roles = await _userService.GetRolesAsync(userId);

            return Ok(roles);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                Message = "User was not found."
            });
        }
    }

    private IActionResult ToUserManagementResult(IdentityResult result, string successMessage)
    {
        if (result.Succeeded)
        {
            return Ok(new { Message = successMessage });
        }

        if (result.Errors.Any(error => error.Code == "UserNotFound"))
        {
            return NotFound(new { Message = "User was not found." });
        }

        return BadRequest(new
        {
            Message = "The user operation could not be completed.",
            Errors = result.Errors
        });
    }
}