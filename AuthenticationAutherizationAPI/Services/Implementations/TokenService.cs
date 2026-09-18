using AuthenticationAutherizationAPI.Configuration;
using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Repositories.Interfaces;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(UserManager<ApplicationUser> userManager, IRefreshTokenRepository refreshTokenRepository, IOptions<JwtSettings> jwtOptions)
    {
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthenticationResponse> GenerateTokensAsync(ApplicationUser user)
    {
        var accessTokenExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

        var refreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        var accessToken = await GenerateAccessTokenAsync(user, accessTokenExpiresAtUtc);

        var refreshToken = GenerateRefreshToken();

        var refreshTokenHash = ComputeSha256Hash(refreshToken);

        var refreshTokenEntity = new RefreshToken
        {
            TokenHash = refreshTokenHash,
            UserId = user.Id,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = refreshTokenExpiresAtUtc
        };

        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        await _refreshTokenRepository.SaveChangesAsync();

        return new AuthenticationResponse
        {
            UserID = user.Id,
            AccessToken = accessToken,
            AccessTokenExpiresAt = ConvertToPakistanTimeZone(accessTokenExpiresAtUtc),
            RefreshToken = refreshToken,
            RefreshTokenExpiresAt = ConvertToPakistanTimeZone(refreshTokenExpiresAtUtc)
        };
    }

    public async Task<AuthenticationResponse?> RotateRefreshTokenAsync(string refreshToken)
    {
        var tokenHash = ComputeSha256Hash(refreshToken);

        var storedToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash);

        if (storedToken is null || !storedToken.IsActive)
        {
            return null;
        }

        var user = storedToken.User;

        storedToken.RevokedAtUtc = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();

        var newRefreshTokenHash = ComputeSha256Hash(newRefreshToken);

        storedToken.ReplacedByTokenHash = newRefreshTokenHash;

        var accessTokenExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

        var refreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

        var accessToken = await GenerateAccessTokenAsync(user, accessTokenExpiresAtUtc);

        var newRefreshTokenEntity = new RefreshToken
        {
            TokenHash = newRefreshTokenHash,
            UserId = user.Id,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = refreshTokenExpiresAtUtc
        };

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);

        await _refreshTokenRepository.SaveChangesAsync();

        return new AuthenticationResponse
        {
            UserID = user.Id,
            AccessToken = accessToken,
            AccessTokenExpiresAt = ConvertToPakistanTimeZone(accessTokenExpiresAtUtc),
            RefreshToken = newRefreshToken,
            RefreshTokenExpiresAt = ConvertToPakistanTimeZone(refreshTokenExpiresAtUtc)
        };
    }

    public Task<string> GeneratePendingMfaTokenAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, user.Id),
        new("purpose", "mfa_pending"),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(59),
            signingCredentials: credentials);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public ClaimsPrincipal? ValidatePendingMfaToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

            var purpose = principal.FindFirst("purpose")?.Value;

            return purpose == "mfa_pending" ? principal : null;
        }
        catch
        {
            return null;
        }
    }

    #region Private Methods

    private async Task<string> GenerateAccessTokenAsync(ApplicationUser user, DateTime expiresAt)
    {
        var claims = new List<Claim> 
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var userClaims = await _userManager.GetClaimsAsync(user);

        claims.AddRange(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes);
    }

    private static string ComputeSha256Hash(string value)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));

        return Convert.ToHexString(bytes);
    }

    private   DateTime ConvertToPakistanTimeZone(DateTime convertDateTime)
    {
        var pakistanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

        var UtcToPKRTime = TimeZoneInfo.ConvertTimeFromUtc(convertDateTime, pakistanTimeZone);
        return UtcToPKRTime;
    }

    #endregion
}