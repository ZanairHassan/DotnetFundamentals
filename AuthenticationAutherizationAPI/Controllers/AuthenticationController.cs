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
        var isValid = await _signInService.ValidateCredentialsAsync(request);

        if (!isValid)
        {
            return Unauthorized(new
            {
                Message = "Try Again, Invalid username or password."
            });
        }

        return Ok(new
        {
            Message = "Credentials are valid. Welcome Home Dear"
        });
    }
}