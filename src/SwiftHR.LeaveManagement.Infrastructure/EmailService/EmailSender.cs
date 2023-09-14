using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SwiftHR.LeaveManagement.Application.Interfaces.Email;
using SwiftHR.LeaveManagement.Application.Models.Email;

namespace SwiftHR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        ArgumentNullException.ThrowIfNull(emailSettings, nameof(emailSettings));
        _emailSettings = emailSettings.Value;
    }

    public EmailSettings _emailSettings { get; }

    /// <summary>
    ///     Gets the email to be sent, and send it and return true if succeeded otherwise false
    /// </summary>
    /// <param name="email">Object of EmailMessage type</param>
    /// <returns>bool</returns>
    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromName,
            Name = _emailSettings.FromAddress
        };

        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}