using AuthenticationAutherizationAPI.DTOs.Claims;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Interfaces;

public interface IClaimService
{
    Task<IdentityResult> AddClaimAsync(string userId, AddClaimRequest request);

    Task<IList<ClaimResponse>> GetClaimsAsync(string userId);

    Task<IdentityResult> RemoveClaimAsync(string userId, string claimType, string claimValue);
}