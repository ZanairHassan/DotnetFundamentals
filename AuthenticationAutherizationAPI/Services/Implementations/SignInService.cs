using AuthenticationAutherizationAPI.Data;
using AuthenticationAutherizationAPI.DTOs.Authentication;
using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class SignInService : ISignInService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMfaService _mfaService;
    private readonly ApplicationDbContext _context;

    public SignInService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMfaService mfaService, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mfaService = mfaService;
        _context = context;
    }
    public async Task<AuthSignInResult> SignInAsync(LoginRequest request)
    {
        var tenant = await _context.Tenants.SingleOrDefaultAsync(x => x.TenantKey == request.TenantKey);

        if (tenant is null)
        {
            return new AuthSignInResult.Failed("Invalid username or password.");
        }

        var user = await _context.Users.SingleOrDefaultAsync(x =>
                x.TenantId == tenant.Id &&
                (x.UserName == request.UserName ||
                 x.Email == request.UserName));

        if (user is null)
        {
            return new AuthSignInResult.Failed("Invalid username or password.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            // Same message as Failed, deliberately - avoids leaking whether the username exists
            return new AuthSignInResult.LockedOut("Invalid username or password.");
        }

        if (result.IsNotAllowed)
        {
            return new AuthSignInResult.NotAllowed("Sign-in is not allowed for this account. Please verify account status or confirm your email.");
        }

        if (!result.Succeeded)
        {
            return new AuthSignInResult.Failed("Invalid username or password.");
        }

        if (await _userManager.GetTwoFactorEnabledAsync(user))
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return new AuthSignInResult.Failed("MFA is enabled but the account does not have an email address.");
            }

            var pendingToken = await _tokenService.GeneratePendingMfaTokenAsync(user);

            await _mfaService.SendOtpAsync(user);

            return new AuthSignInResult.RequiresMfa(pendingToken);
        }

        var tokens = await _tokenService.GenerateTokensAsync(user);

        return new AuthSignInResult.Success(tokens);
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

    public async Task<AuthenticationResponse?> VerifyMfaAsync(VerifyMfaRequest request)
    {
        var principal = _tokenService.ValidatePendingMfaToken(request.PendingMfaToken);

        var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            return null;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return null;
        }

        var isValid = await _mfaService.VerifyOtpAsync(user, request.Code);

        return isValid ? await _tokenService.GenerateTokensAsync(user) : null;
    }
}