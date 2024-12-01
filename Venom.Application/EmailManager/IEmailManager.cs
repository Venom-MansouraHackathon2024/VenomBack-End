using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Auth.Dto;

namespace Venom.Application.EmailService
{
    public interface IEmailManager
    {
        Task<GeneralAuthResponse> SendEmailAsync(string recipientEmail, string subject, string body);

    }

}
