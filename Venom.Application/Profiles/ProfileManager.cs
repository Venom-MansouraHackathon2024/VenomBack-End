using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.Profiles.Dtos;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;

namespace Venom.Application.Profile
{
    public class ProfileManager : IProfileManager
    { 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IprofileRepo _profileRepository;

        public ProfileManager(UserManager<ApplicationUser> userManager, IMapper mapper, IprofileRepo profileRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        public Task<bool> DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<GeneralResponseDto> DeleteAsync(string email)
        {
            var user = await _profileRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return new GeneralResponseDto
                {
                    Message = "No user with this email",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            var IsDeleted = await _profileRepository.DeleteAsync(user);
            if (IsDeleted)
            {
                return new GeneralResponseDto
                {
                    Message = "User deleted successfully",
                    IsSucceeded = true,
                    StatusCode = 200
                };
            }
            else
                return new GeneralResponseDto
                {
                    Message = "An error occurred during deletion",
                    IsSucceeded = false,
                    StatusCode = 500
                };
        }


        public async Task<GeneralResponseDto> GetByEmailAsync(string email)
        {
            var user = await _profileRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return new GeneralResponseDto
                {
                    Message = "No user with this Email",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }
            return new GeneralResponseDto
            {
                IsSucceeded = true,
                StatusCode = 200,
                Model = _mapper.Map<ProfileReadDto>(user)
            };
        }
        public async Task<GeneralResponseDto> UpdateAsync(ProfileUpdateDto profileUpdateDto)
        {
            var existingUser = await _profileRepository.GetByEmailAsync(profileUpdateDto.Email);
            if (existingUser == null)
            {
                return new GeneralResponseDto
                {
                    Message = "No user with this Email",
                    IsSucceeded = false,
                    StatusCode = 404
                };
            }

            if (!string.IsNullOrWhiteSpace(profileUpdateDto.ProfilePicture))
            {
                existingUser.ProfilePicture = profileUpdateDto.ProfilePicture;
            }

            if (!string.IsNullOrWhiteSpace(profileUpdateDto.UserName))
            {
                existingUser.UserName = profileUpdateDto.UserName;
            }

            if (!string.IsNullOrWhiteSpace(profileUpdateDto.Email) && profileUpdateDto.Email != existingUser.Email)
            {
                existingUser.Email = profileUpdateDto.Email;
            }

            var updatedProfile = await _profileRepository.UpdateAsync(existingUser);

            return new GeneralResponseDto
            {
                IsSucceeded = true,
                StatusCode = 200,
                Model = _mapper.Map<ProfileReadDto>(updatedProfile)
            };
        }
    }
}
