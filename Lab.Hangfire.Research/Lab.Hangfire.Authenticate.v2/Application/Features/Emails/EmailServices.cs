using Application.DTOs.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.Features.Emails
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

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                var email = new MimeMessage();

                //email.Sender = MailboxAddress.Parse(request.From ?? _configuration["MailSettings:EmailFrom"]);
                email.From.Add(new MailboxAddress(_configuration["MailSettings:DisplayName"], request.From ?? _configuration["MailSettings:EmailFrom"]));

                email.To.Add(MailboxAddress.Parse(request.To));

                //email.Cc.Add(MailboxAddress.Parse(request.To));

                email.Subject = request.Subject;

                var builder = new BodyBuilder();

                //builder.HtmlBody = request.Body;
                //string html = $"<p>Xin chao Vinh, hom nay la {DateTime.Now.ToString("dd-MM-yyyy")}</p>";
                //builder.HtmlBody = html;

                string fileName = "MailTemplate.html";
                string path = Path.Combine(_configuration["DefaultPathTemplate"], fileName);
                using (StreamReader SourceReader = File.OpenText(path))
                {
                    string html = SourceReader.ReadToEnd();
                    //string textHtml = "<p>Xin chao cac ban, minh la {0} ne!</p>";
                    builder.HtmlBody = string.Format(html, "Quang Vinh", "Esuhai Group");
                }

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
