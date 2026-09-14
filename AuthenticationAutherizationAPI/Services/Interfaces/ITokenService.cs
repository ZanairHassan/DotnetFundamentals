using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ITokenService
{
    Task<AuthenticationResponse> GenerateTokensAsync(ApplicationUser user);

    Task<AuthenticationResponse?> RotateRefreshTokenAsync(string refreshToken);
}