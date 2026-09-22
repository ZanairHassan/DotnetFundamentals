using AuthenticationAutherizationAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class CustomSignInManager : SignInManager<ApplicationUser>
{
    private readonly ILogger<CustomSignInManager> _customLogger;

    public CustomSignInManager(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation,
        ILogger<CustomSignInManager> customLogger)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        _customLogger = customLogger;
    }

    public override async Task<bool> CanSignInAsync(ApplicationUser user)
    {
        if (!await base.CanSignInAsync(user))
        {
            _customLogger.LogWarning("Sign-in check failed for user {UserId} ({UserName}). User cannot sign in based on identity policy.", user.Id, user.UserName);
            return false;
        }

        if (await UserManager.IsLockedOutAsync(user))
        {
            _customLogger.LogWarning("Sign-in rejected: User {UserName} is currently locked out.", user.UserName);
            return false;
        }

        return true;
    }

    public override async Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
    {
        var httpContext = Context;
        var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

        _customLogger.LogInformation("Processing password sign-in attempt for user {UserName} from IP: {IpAddress}", user.UserName, ipAddress);

        if (!await CanSignInAsync(user))
        {
            _customLogger.LogWarning("Sign-in not allowed for user {UserName} from IP: {IpAddress}", user.UserName, ipAddress);
            return SignInResult.NotAllowed;
        }

        var result = await base.CheckPasswordSignInAsync(user, password, lockoutOnFailure);

        if (result.Succeeded)
        {
            _customLogger.LogInformation("Sign-in succeeded for user {UserName} from IP: {IpAddress}", user.UserName, ipAddress);
        }
        else if (result.IsLockedOut)
        {
            _customLogger.LogWarning("Account locked out for user {UserName} from IP: {IpAddress}. Lockout end: {LockoutEnd}",
                user.UserName, ipAddress, user.LockoutEnd);
        }
        else if (result.IsNotAllowed)
        {
            _customLogger.LogWarning("Sign-in not allowed for user {UserName} from IP: {IpAddress}", user.UserName, ipAddress);
        }
        else if (result.RequiresTwoFactor)
        {
            _customLogger.LogInformation("Sign-in requires two-factor authentication for user {UserName} from IP: {IpAddress}", user.UserName, ipAddress);
        }
        else
        {
            _customLogger.LogWarning("Invalid password attempt for user {UserName} from IP: {IpAddress}. Failed attempts: {AccessFailedCount}",
                user.UserName, ipAddress, user.AccessFailedCount);
        }

        return result;
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName) ?? await UserManager.FindByEmailAsync(userName);

        if (user is null)
        {
            _customLogger.LogWarning("Password sign-in failed: No user found for identifier {Identifier}", userName);
            return SignInResult.Failed;
        }

        return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }

    public async Task<SignInResult> PasswordSignInWithEmailOrUsernameAsync(string identifier, string password, bool lockoutOnFailure = true)
    {
        var user = await UserManager.FindByNameAsync(identifier) ?? await UserManager.FindByEmailAsync(identifier);

        if (user is null)
        {
            _customLogger.LogWarning("Sign-in failed: User identifier {Identifier} not found.", identifier);
            return SignInResult.Failed;
        }

        return await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }
}
