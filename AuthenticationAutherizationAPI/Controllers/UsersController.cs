using AuthenticationAutherizationAPI.DTOs.Users;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("{userId}/roles")]
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

    [HttpGet("{userId}/roles")]
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
}