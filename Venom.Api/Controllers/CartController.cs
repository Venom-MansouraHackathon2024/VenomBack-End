using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.Carts;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartManager _cartManager;
        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet]
        // [Authorize(Roles ="Admin")]

        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cartManager.GetAll();
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model });
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var result = await _cartManager.GetByIdAsync(Id);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model });
        }
        [HttpGet("UserEmail/{UserEmail}")]
        public async Task<IActionResult> GetByUserAsync(string UserEmail)
        {
            var result = await _cartManager.GetByUserEmailAsync(UserEmail);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model });
        
        }


    }
}
