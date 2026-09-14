using AuthenticationAutherizationAPI.DTOs.Authentication;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ISignInService
{
    Task<AuthenticationResponse?> SignInAsync(LoginRequest request);

    Task<AuthenticationResponse?> RefreshTokenAsync(RefreshTokenRequest request);
}