using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;
using Venom.Application.Auth.Dto;

namespace Venom.Application.EmailService
{
    public class EmailManager : IEmailManager
    { 
        private readonly SmtpSettings _smtpSettings;

        public EmailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<GeneralAuthResponse> SendEmailAsync(string recipientEmail, string subject, string body)
        {
            GeneralAuthResponse response = new GeneralAuthResponse();

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.UserName, "Gdg Forget Password"),
                    To = { new MailAddress(recipientEmail) },
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                using (var smtp = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }

                response.IsSucceeded = true;
                response.Message = "Email sent successfully.";
            }
            catch (Exception ex)
            {
                response.IsSucceeded = false;
                response.Message = $"Failed to send email: {ex.Message}";
            }

            return response;
        }
    }
}

