using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash);

    Task SaveChangesAsync();
}