using AuthenticationAutherizationAPI.Configuration;
using AuthenticationAutherizationAPI.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AuthenticationAutherizationAPI.Services.Implementations;

public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<SmtpEmailSender> _logger;

    public SmtpEmailSender(IOptions<SmtpSettings> options, ILogger<SmtpEmailSender> logger)
    {
        _settings = options.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("Recipient email address is required.", nameof(toEmail));
        }

        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);

        using var smtpClient = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.Username, _settings.Password)
        };

        try
        {
            await smtpClient.SendMailAsync(message);

            _logger.LogInformation("Email successfully sent to {EmailAddress}", toEmail);
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "Failed to send email to {EmailAddress}", toEmail);

            throw new InvalidOperationException("Failed to send email.", ex);
        }
    }
}