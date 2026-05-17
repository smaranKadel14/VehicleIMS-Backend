using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using VehicleIMS.Application.Interfaces.IServices;

namespace VehicleIMS.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public SmtpEmailService(IOptions<SmtpSettings> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            if (string.IsNullOrWhiteSpace(_smtpSettings.Host) ||
                string.IsNullOrWhiteSpace(_smtpSettings.FromEmail) ||
                string.IsNullOrWhiteSpace(_smtpSettings.Username) ||
                string.IsNullOrWhiteSpace(_smtpSettings.Password))
            {
                throw new InvalidOperationException("SMTP settings are not configured correctly.");
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            await client.SendMailAsync(message);
        }
    }
}