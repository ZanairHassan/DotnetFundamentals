using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IMfaService
{
    Task SendOtpAsync(ApplicationUser user);
    Task<bool> VerifyOtpAsync(ApplicationUser user, string code);
    Task<IdentityResult> EnableMfaAsync(string userId);
    Task<IdentityResult> DisableMfaAsync(string userId);
}