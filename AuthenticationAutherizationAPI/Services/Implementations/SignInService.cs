using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class SignInService : ISignInService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public SignInService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AuthenticationResponse?> SignInAsync(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            return null;
        }

        return await _tokenService.GenerateTokensAsync(user);
    }

    public async Task<AuthenticationResponse?> RefreshTokenAsync(RefreshTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return null;
        }

        return await _tokenService.RotateRefreshTokenAsync(request.RefreshToken);
    }

    public AuthenticationProperties ConfigureExternalLoginAsync(string provider, string redirectUrl)
    {
        return  _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    }

    public async Task<AuthenticationResponse?> HandleExternalLoginAsync(string provider)
    {
        var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

        if (externalLoginInfo is null)
        {
            return null;
        }

        if (!string.Equals(externalLoginInfo.LoginProvider, provider, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var signInResult =
            await _signInManager.ExternalLoginSignInAsync(
                externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey,
                isPersistent: false,
                bypassTwoFactor: true);

        if (signInResult.Succeeded)
        {
            var existingUser =
                await _userManager.FindByLoginAsync(
                    externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey);

            if (existingUser is null)
            {
                return null;
            }

            return await _tokenService.GenerateTokensAsync(existingUser);
        }

        if (signInResult.IsLockedOut)
        {
            return null;
        }

        var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(user);

            if (!createResult.Succeeded)
            {
                return null;
            }
        }

        var addLoginResult = await _userManager.AddLoginAsync(user,
                new UserLoginInfo(
                    externalLoginInfo.LoginProvider,
                    externalLoginInfo.ProviderKey,
                    externalLoginInfo.ProviderDisplayName));

        if (!addLoginResult.Succeeded)
        {
            return null;
        }

        return await _tokenService.GenerateTokensAsync(user);
    }
}