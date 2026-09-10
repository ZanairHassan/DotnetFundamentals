using AuthenticationAutherizationAPI.DTOs.Authentication;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ISignInService
{
    Task<bool> ValidateCredentialsAsync(LoginRequest request);
}