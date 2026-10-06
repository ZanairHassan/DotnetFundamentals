using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ISignInService _signInService;

    public AuthenticationController(ISignInService signInService)
    {
        _signInService = signInService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _signInService.SignInAsync(request);

        if (response is null)
        {
            return Unauthorized(new
            {
                Message = "Invalid username or password."
            });
        }

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        var response = await _signInService.RefreshTokenAsync(request);

        if (response is null)
        {
            return Unauthorized(new
            {
                Message = "Invalid or expired refresh token."
            });
        }

        return Ok(response);
    }

    [HttpGet("GoogleLogin")]
    public IActionResult ExternalLogin([FromQuery] string provider, string returnUrl = "/")
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Authentication",
            new
            {
                returnUrl,
                provider
            });

        var properties =  _signInService.ConfigureExternalLoginAsync(provider, redirectUrl!);

        return Challenge(properties, provider);
    }

    [HttpGet("GoogleLoginCallback")]
    public async Task<IActionResult> ExternalLoginCallback(string provider, string returnUrl = "/")
    {
        var response = await _signInService.HandleExternalLoginAsync(provider);

        if (response is null)
        {
            return Unauthorized(new
            {
                Message = "External authentication failed."
            });
        }

        return Ok(response);
    }
}