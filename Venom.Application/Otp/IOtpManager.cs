using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Application.Otp
{
    public interface IOtpManager
    {
        Task<string> GenerateOtpAsync(string email);
        Task RemoveOtpAsync(string email);
    }
}
