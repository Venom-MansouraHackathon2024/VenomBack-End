using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.Profiles.Dtos;
using Venom.Application.Profile;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileManager _profileManager;

        public ProfileController(IProfileManager profileManager)
        {
            _profileManager = profileManager;
        }


        [HttpGet("UserEmail/{email}")]

        public async Task<IActionResult> GetByEmail(string email)
        {

            var result = await _profileManager.GetByEmailAsync(email);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.Message, result.StatusCode });
            }
            return Ok(new { result.Model, result.StatusCode });
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProfileUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _profileManager.UpdateAsync(dto);
            if (!result.IsSucceeded)
            {
                return BadRequest(new { result.Message, result.StatusCode });
            }
            return Ok(new { result.Model, result.StatusCode });
        }
      

       
        [HttpDelete("Email/{email}")]
        public async Task<IActionResult> Delete( string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _profileManager.DeleteAsync(email);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.Message, result.StatusCode });
            }
            
            return Ok(new { result.Message, result.StatusCode });
        }
    }
}
