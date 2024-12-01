using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.Auth;
using Venom.Application.Auth.Dto;
using Venom.Application.Otp.OtpDtos;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _authManager.Register(registerDto);
                if (result.IsSucceeded == false)
                {
                    return BadRequest(new { result.Message, StatusCode = 400 });
                }
                return Ok(new { result.Token, result.ExpireDate, result.Email, result.UserName, result.Id, StatusCode = 200 });
            }
            return BadRequest(new { ModelState, StatusCode = 400 });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authManager.Login(loginDto);
            if (result.IsSucceeded == false)
            {
                return Unauthorized(new { result.Message, StatusCode = 401 });
            }
            return Ok(new { result.Token, result.ExpireDate, result.Email, result.UserName, result.Id, StatusCode = 200 });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] SendOtpRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }
            var response = await _authManager.SendOtpForPasswordReset(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }
            return Ok(new { response.Message, statusCode = 200 });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }

            var response = await _authManager.VerifyOtp(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }
            return Ok(new { response.Message, statusCode = 200 });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordWithOtp([FromBody] ResetPasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { ModelState, StatusCode = 400 });
            }

            var response = await _authManager.ResetPasswordWithOtp(dto);
            if (!response.IsSucceeded)
            {
                return BadRequest(new { response.Message, StatusCode = 400 });
            }

            return Ok(new
            {
                token = response.Token,
                expireDate = response.ExpireDate,
                message = response.Message,
                StatusCode = 200
            });
        }



    }
}
