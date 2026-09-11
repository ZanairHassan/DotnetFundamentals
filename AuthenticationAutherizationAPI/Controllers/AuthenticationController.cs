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
        var user = await _signInService.ValidateCredentialsAsync(request);

        if (user is null)
        {
            return Unauthorized(new
            {
                Message = "Invalid username or password."
            });
        }

        var accessToken = await _tokenService.GenerateAccessTokenAsync(user);

        var refreshToken = await _tokenService.GenerateAndStoreRefreshTokenAsync(user);

        return Ok(new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAtUtc = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(7)
        });
    }
}