

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Venom.Application.Auth.Dto;
using Venom.Application.Carts.Dtos;
using Venom.Application.Dtos;
using Venom.Application.EmailService;
using Venom.Application.Otp;
using Venom.Application.Otp.OtpDtos;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Auth
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IOtpManager _otpManager;
        private readonly IMemoryCache _cache;
        private readonly IEmailManager _emailManager;
        private readonly IMapper _mapper;
        private readonly ICartRepo _cartRepo;
        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration configuration, IOtpManager otpManager, IMemoryCache cache, IEmailManager emailManager, IMapper mapper = null, ICartRepo cartRepo = null)
        {
            _userManager = userManager;
            _configuration = configuration;
            _otpManager = otpManager;
            _cache = cache;
            _emailManager = emailManager;
            _mapper = mapper;
            _cartRepo = cartRepo;
        }

        public async Task<GeneralAuthResponse> Login(LoginDto loginDto)
        {
            GeneralAuthResponse generalAuthResponse = new GeneralAuthResponse();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "Email or Password Is Incorrect";
                return generalAuthResponse;
            }

            var claims = await _userManager.GetClaimsAsync(user);

            generalAuthResponse = GeneralToken(claims);
            generalAuthResponse.IsSucceeded = true;
            generalAuthResponse.Email = user.Email;
            generalAuthResponse.Id = user.Id;
            generalAuthResponse.UserName = user.UserName;
            return generalAuthResponse;
        }

        public async Task<GeneralAuthResponse> Register(RegisterDto registerDto)
        {
            GeneralAuthResponse generalAuthResponse = new GeneralAuthResponse();

            if (registerDto.Password != registerDto.ConfirmPassWord)
            {
                generalAuthResponse.Message = "ConfirmPassword and Password do not match.";
                generalAuthResponse.IsSucceeded = false;
                return generalAuthResponse;
            }

            var emailExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (emailExists != null)
            {
                generalAuthResponse.Message = "The Email Address Is Already Exists";
                generalAuthResponse.IsSucceeded = false;
                return generalAuthResponse;
            }


            ApplicationUser AppUser = new ApplicationUser();

            AppUser.UserName = registerDto.UserName;
            AppUser.Email = registerDto.Email;
            IdentityResult identityResult = await _userManager.CreateAsync(AppUser, registerDto.Password);

            if (!identityResult.Succeeded)
            {
                generalAuthResponse.Message = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                generalAuthResponse.IsSucceeded = false;
                return generalAuthResponse;
            }
            var roleResult = await _userManager.AddToRoleAsync(AppUser, "User");
            var addCartDto = new AddCartDto
            {
                UserId = AppUser.Id
            };
            var cart = _mapper.Map<Cart>(addCartDto);

            await _cartRepo.AddAsync(cart);

            await _cartRepo.SaveChangesAsync();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, AppUser.Id));
            claims.Add(new Claim(ClaimTypes.Name, AppUser.UserName));
            claims.Add(new Claim(ClaimTypes.Email, AppUser.Email));



            await _userManager.AddClaimsAsync(AppUser, claims);

            generalAuthResponse = GeneralToken(claims);
            generalAuthResponse.IsSucceeded = true;
            generalAuthResponse.Email = AppUser.Email;
            generalAuthResponse.Id = AppUser.Id;
            generalAuthResponse.UserName = AppUser.UserName;
            return generalAuthResponse;
        }

        public async Task<GeneralAuthResponse> ResetPasswordWithOtp(ResetPasswordRequestDto dto)
        {
            GeneralAuthResponse generalAuthResponse = new GeneralAuthResponse();

            if (!_cache.TryGetValue($"{dto.Email}_Verified", out bool otpVerified) || !otpVerified)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "Invalid or expired OTP";
                return generalAuthResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "User not found";
                return generalAuthResponse;
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.Password);
            if (!result.Succeeded)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = string.Join(", ", result.Errors.Select(e => e.Description));
                return generalAuthResponse;
            }

            await _otpManager.RemoveOtpAsync(dto.Email);

            var claims = await _userManager.GetClaimsAsync(user);
            generalAuthResponse = GeneralToken(claims);
            generalAuthResponse.IsSucceeded = true;
            generalAuthResponse.Message = "Password reset successfully";
            return generalAuthResponse;
        }

        public async Task<GeneralAuthResponse> SendOtpForPasswordReset(SendOtpRequestDto dto)
        {
            GeneralAuthResponse generalAuthResponse = new GeneralAuthResponse();

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "User not found";
                return generalAuthResponse;
            }

            var otpCode = await _otpManager.GenerateOtpAsync(dto.Email);


            var emailResponse = await _emailManager.SendEmailAsync(user.Email, "Your Password Reset OTP Code", $"Your OTP code for resetting your password is: {otpCode}");
            if (!emailResponse.IsSucceeded)
            {
                return emailResponse; // Return the email error if sending failed
            }

            generalAuthResponse.Message = "OTP sent successfully.";
            generalAuthResponse.IsSucceeded = emailResponse.IsSucceeded;
            return generalAuthResponse;
        }

        public async Task<GeneralAuthResponse> VerifyOtp(VerifyOtpRequestDto dto)
        {
            GeneralAuthResponse generalAuthResponse = new GeneralAuthResponse();

            // Retrieve email from cache using OTP
            if (!_cache.TryGetValue($"{dto.Email}_Verified", out string storedOtp) || storedOtp != dto.Otp)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "Invalid or expired OTP";
                return generalAuthResponse;
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                generalAuthResponse.IsSucceeded = false;
                generalAuthResponse.Message = "User not found";
                return generalAuthResponse;
            }

            _cache.Set($"{dto.Email}_Verified", true, TimeSpan.FromMinutes(10));

            generalAuthResponse.IsSucceeded = true;
            generalAuthResponse.Message = "OTP verified successfully. You can now reset your password.";
            return generalAuthResponse;
        }

        private GeneralAuthResponse GeneralToken(IList<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("SecretKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
    );

            //    var securityKeyOfString = _configuration.GetSection("Key").Value;
            //  var securityKeyOfBytes = Encoding.ASCII.GetBytes(securityKeyOfString);
            //var securityKey = new SymmetricSecurityKey(securityKeyOfBytes);

            //    SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.Now.AddDays(2);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: expireDate,
                issuer: _configuration["Jwt:Issuer"],
                audience : _configuration["Jwt:Audience"],
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return new GeneralAuthResponse
            {
                Token = token,
                ExpireDate = expireDate
            };
        }
    }
}