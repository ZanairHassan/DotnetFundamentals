using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessTokenAsync(ApplicationUser user);

    Task<string> GenerateAndStoreRefreshTokenAsync(ApplicationUser user);
}