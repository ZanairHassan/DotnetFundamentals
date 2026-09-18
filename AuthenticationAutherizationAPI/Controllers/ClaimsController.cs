using AuthenticationAutherizationAPI.DTOs.Claims;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class ClaimsController : ControllerBase
{
    private readonly IClaimService _claimService;

    public ClaimsController(IClaimService claimService)
    {
        _claimService = claimService;
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddClaim(string userId, AddClaimRequest request)
    {
        var result = await _claimService.AddClaimAsync(userId, request);

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
                Message = "Unable to add claim.",
                Errors = result.Errors
            });
        }

        return StatusCode(StatusCodes.Status201Created, new
        {
            Message = "Claim added successfully."
        });
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetClaims(string userId)
    {
        try
        {
            var claims = await _claimService.GetClaimsAsync(userId);

            return Ok(claims);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                Message = "User was not found."
            });
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveClaim(string userId, [FromQuery] string claimType, [FromQuery] string claimValue)
    {
        var result = await _claimService.RemoveClaimAsync(userId, claimType, claimValue);

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
                Message = "Unable to remove claim.",
                Errors = result.Errors
            });
        }

        return Ok(new
        {
            Message = "Claim removed successfully."
        });
    }
}