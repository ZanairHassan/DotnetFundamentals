using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAutherizationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ISignInService _signInService;
    private readonly ITokenService _tokenService;

    public AuthenticationController(ISignInService signInService, ITokenService tokenService)
    {
        _signInService = signInService;
        _tokenService = tokenService;
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
}