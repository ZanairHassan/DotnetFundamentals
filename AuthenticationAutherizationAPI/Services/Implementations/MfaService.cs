using AuthenticationAutherizationAPI.Models;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class MfaService : IMfaService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public MfaService(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task SendOtpAsync(ApplicationUser user)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new InvalidOperationException("User does not have an email address.");
        }

        var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);

        var htmlMessage = $"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="UTF-8">
                <title>MFA Verification Code</title>
            </head>

            <body style="
                margin: 0;
                padding: 0;
                background-color: #f4f6f8;
                font-family: Arial, Helvetica, sans-serif;
            ">

                <div style="
                    max-width: 600px;
                    margin: 40px auto;
                    background-color: #ffffff;
                    border-radius: 10px;
                    padding: 40px;
                    box-shadow: 0 2px 8px rgba(0,0,0,0.08);
                ">

                    <h1 style="
                        margin-top: 0;
                        color: #222222;
                        text-align: center;
                    ">
                        Authentication Authorization API
                    </h1>

                    <h2 style="
                        color: #333333;
                        text-align: center;
                    ">
                        Multi-Factor Authentication
                    </h2>

                    <p style="
                        color: #555555;
                        font-size: 16px;
                        line-height: 1.6;
                    ">
                        We received a request to sign in to your account.
                    </p>

                    <p style="
                        color: #555555;
                        font-size: 16px;
                        line-height: 1.6;
                    ">
                        Use the following verification code to complete
                        your sign-in:
                    </p>

                    <div style="
                        margin: 30px 0;
                        padding: 20px;
                        background-color: #f1f3f5;
                        border-radius: 8px;
                        text-align: center;
                    ">

                        <span style="
                            font-size: 32px;
                            font-weight: bold;
                            letter-spacing: 8px;
                            color: #222222;
                        ">
                            {code}
                        </span>

                    </div>

                    <p style="
                        color: #555555;
                        font-size: 14px;
                        line-height: 1.6;
                    ">
                        Enter this code in the application to complete
                        authentication.
                    </p>

                    <p style="
                        color: #777777;
                        font-size: 14px;
                        line-height: 1.6;
                    ">
                        If you did not attempt to sign in, you can safely
                        ignore this email.
                    </p>

                    <hr style="
                        border: 0;
                        border-top: 1px solid #eeeeee;
                        margin: 30px 0;
                    ">

                    <p style="
                        color: #999999;
                        font-size: 12px;
                        text-align: center;
                    ">
                        This is an automated email. Please do not reply.
                    </p>

                </div>

            </body>
            </html>
            """;

        await _emailSender.SendEmailAsync(user.Email, "Your MFA Verification Code", htmlMessage);
    }

    public async Task<bool> VerifyOtpAsync(ApplicationUser user, string code)
    {
        if (!await _userManager.GetTwoFactorEnabledAsync(user))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        return await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, code);
    }

    public async Task<IdentityResult> EnableMfaAsync(string userId)
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

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            return IdentityResult.Failed(
                new IdentityError
                {
                    Code = "EmailRequired",
                    Description = "A valid email address is required to enable MFA."
                });
        }

        return await _userManager.SetTwoFactorEnabledAsync(user, true);
    }

    public async Task<IdentityResult> DisableMfaAsync(string userId)
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

        return await _userManager.SetTwoFactorEnabledAsync(user, false);
    }
}