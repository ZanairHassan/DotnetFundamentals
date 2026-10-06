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
        var result = await _signInService.SignInAsync(request);

        return result switch
        {
            AuthSignInResult.Success s => Ok(s.Tokens),
            AuthSignInResult.RequiresMfa m => Ok(new { requiresMfa = true, pendingMfaToken = m.PendingMfaToken }),
            AuthSignInResult.Failed f => Unauthorized(new { Message = f.Reason }),
            AuthSignInResult.LockedOut l => Unauthorized(new { Message = l.Reason }),
            _ => StatusCode(500)
        };
    }

    [HttpPost("mfaVerify")]
    public async Task<IActionResult> VerifyMfa(VerifyMfaRequest request)
    {
        var response = await _signInService.VerifyMfaAsync(request);

        if (response is null)
        {
            return Unauthorized(new { Message = "Invalid or expired code." });
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