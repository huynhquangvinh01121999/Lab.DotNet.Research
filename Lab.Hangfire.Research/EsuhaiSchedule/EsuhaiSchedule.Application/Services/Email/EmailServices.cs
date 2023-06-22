using EsuhaiSchedule.Application.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace EsuhaiSchedule.Application.Services.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        public ILogger<EmailServices> _logger { get; }

        public EmailServices(IConfiguration configuration, ILogger<EmailServices> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendAsync(EmailDtos request)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_configuration["MailSettings:DisplayName"], request.From ?? _configuration["MailSettings:EmailFrom"]));

                email.To.Add(MailboxAddress.Parse(request.To));

                var mailCc = _configuration["MailSettings:EmailCc"];
                if (!string.IsNullOrEmpty(mailCc))
                    email.Cc.Add(MailboxAddress.Parse(mailCc));

                email.Subject = request.Subject;

                var builder = new BodyBuilder();

                builder.HtmlBody = request.Body;

                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_configuration["MailSettings:SmtpHost"], int.Parse(_configuration["MailSettings:SmtpPort"]), SecureSocketOptions.StartTlsWhenAvailable);

                    smtp.Authenticate(_configuration["MailSettings:SmtpUser"], _configuration["MailSettings:SmtpPass"]);

                    await smtp.SendAsync(email);

                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
