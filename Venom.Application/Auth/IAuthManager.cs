using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Auth.Dto;
using Venom.Application.Otp.OtpDtos;

namespace Venom.Application.Auth
{
    public interface IAuthManager
    {
        Task<GeneralAuthResponse> Login(LoginDto loginDto);
        Task<GeneralAuthResponse> Register(RegisterDto registerDto);
        Task<GeneralAuthResponse> SendOtpForPasswordReset(SendOtpRequestDto dto);
        Task<GeneralAuthResponse> VerifyOtp(VerifyOtpRequestDto dto);
        Task<GeneralAuthResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto);
    }
    
}
