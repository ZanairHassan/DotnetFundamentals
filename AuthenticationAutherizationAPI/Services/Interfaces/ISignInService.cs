using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ISignInService
{
    Task<ApplicationUser?> ValidateCredentialsAsync(LoginRequest request);
}