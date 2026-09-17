using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;
using System.Security.Claims;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ITokenService
{
    Task<AuthenticationResponse> GenerateTokensAsync(ApplicationUser user);

    Task<AuthenticationResponse?> RotateRefreshTokenAsync(string refreshToken);

    Task<string> GeneratePendingMfaTokenAsync(ApplicationUser user);

    ClaimsPrincipal? ValidatePendingMfaToken(string token);
}