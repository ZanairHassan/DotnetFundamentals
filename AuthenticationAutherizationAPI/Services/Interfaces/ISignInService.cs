using AuthenticationAutherizationAPI.DTOs.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ISignInService
{
    Task<AuthenticationResponse?> SignInAsync(LoginRequest request);

    Task<AuthenticationResponse?> RefreshTokenAsync(RefreshTokenRequest request);

    AuthenticationProperties ConfigureExternalLoginAsync(string provider, string redirectUrl); 
    
    Task<AuthenticationResponse?> HandleExternalLoginAsync(string provider);
}