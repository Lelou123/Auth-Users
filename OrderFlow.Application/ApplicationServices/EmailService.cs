using System.Net;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using OrderFlow.Domain.Abstractions;
using OrderFlow.Domain.Dtos;
using OrderFlow.Domain.Interfaces.Services;
using OrderFlow.Infrastructure.Settings;

namespace OrderFlow.Application.ApplicationServices;

public class EmailService(
    IOptions<EmailSettings> emailSettings
) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task<Result> SendEmail(EmailMessage message)
    {
        if (string.IsNullOrEmpty(message.Content))
        {
            return Result.Failure(EmailErrors.Empty);
        }

        var mimeMessage = CreateBodyEmail(message);

        await Send(mimeMessage);

        return Result.Success();
    }

    private MimeMessage CreateBodyEmail(EmailMessage message)
    {
        MailboxAddress address = new(message.UserName, message.UserEmail);

        string? emailFrom = _emailSettings.From;

        if (emailFrom is null)
        {
            throw new NullReferenceException("Could not retrieve the Email SMTP server info");
        }

        MimeMessage mimeMessage = new();
        mimeMessage.From.Add(new MailboxAddress("Order Flow", emailFrom));
        mimeMessage.To.Add(address);
        mimeMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder {
            HtmlBody = "<p>Hello,</p>" +
                       "<p>This is your activation code:</p>" +
                       "<div style='background-color: #008CBA; border: none; color: white; padding: 12px 24px; text-align: center; " +
                       "text-decoration: none; display: " +
                       "inline-block; font-size: 16px; margin-top: 10px;'>" +
                       $"<h1>{message.Content}</h1>" +
                       "</div>"
        };

        mimeMessage.Body = bodyBuilder.ToMessageBody();

        return mimeMessage;
    }


    private async Task Send(MimeMessage mimeMessage)
    {
        using SmtpClient smtpClient = new();

        string smtpServer = _emailSettings.SmtpServer;
        int port = _emailSettings.Port;
        string emailProviderPass = _emailSettings.Password;
        string fromEmail = _emailSettings.From;

        try
        {
            await smtpClient.ConnectAsync(smtpServer, port, true);

            await smtpClient.AuthenticateAsync(fromEmail, emailProviderPass);

            await smtpClient.SendAsync(mimeMessage);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            await smtpClient.DisconnectAsync(true);
        }
    }
}