using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.Dtos;
using Venom.Application.Profiles.Dtos;

namespace Venom.Application.Profile
{
    public interface IProfileManager
    {
        Task<GeneralResponseDto> GetByEmailAsync(string email);
        Task<GeneralResponseDto> UpdateAsync(ProfileUpdateDto profileUpdateDto);
        Task<GeneralResponseDto> DeleteAsync(string email);
    }
}
