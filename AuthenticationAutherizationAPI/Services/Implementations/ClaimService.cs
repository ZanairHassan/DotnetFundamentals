using AuthenticationAutherizationAPI.DTOs.Claims;
using AuthenticationAutherizationAPI.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AuthenticationAutherizationAPI.Models;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class ClaimService : IClaimService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ClaimService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddClaimAsync(string userId, AddClaimRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User was not found."
                });
        }

        var claim = new Claim(request.ClaimType.Trim(), request.ClaimValue.Trim());

        return await _userManager.AddClaimAsync(
            user,
            claim);
    }

    public async Task<IList<ClaimResponse>> GetClaimsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new KeyNotFoundException("User was not found.");
        }

        var claims = await _userManager.GetClaimsAsync(user);

        return claims
            .Select(claim => new ClaimResponse
            {
                Type = claim.Type,
                Value = claim.Value
            })
            .ToList();
    }

    public async Task<IdentityResult> RemoveClaimAsync(string userId, string claimType, string claimValue)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User was not found."
                });
        }

        var claim = new Claim(claimType.Trim(), claimValue.Trim());

        return await _userManager.RemoveClaimAsync(user, claim);
    }
}