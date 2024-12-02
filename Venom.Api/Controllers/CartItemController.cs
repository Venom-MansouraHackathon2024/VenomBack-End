using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.CartItems;
using Venom.Application.CartItems.Dtos;
using Venom.Application.Dtos;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemManager _cartItemManager;
        public CartItemController(ICartItemManager cartItemManager)
        {
            _cartItemManager = cartItemManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cartItemManager.GetAll();
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model, });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var result = await _cartItemManager.GetByIdAsync(Id);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model, });
        }

        [HttpGet("CartId/{cartId}")]
        public async Task<IActionResult> GetByCartAsync(int cartId)
        {
            var result = await _cartItemManager.GetByCartIdAsync(cartId);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(new { result.StatusCode, result.Model, });
        }


        [HttpPost]
        public async Task<ActionResult<GeneralResponseDto>> AddAsync(AddCartItemDto dto)
        {
            var response = await _cartItemManager.AddAsync(dto);
            if (!response.IsSucceeded)
            {
                return NotFound(new{response.StatusCode, response.Message});
            }

            return Ok(new {  response.StatusCode, response.Model, });
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<GeneralResponseDto>> Update(int id, [FromBody] UpdateCartItemDto dto)
        {
            var response = await _cartItemManager.UpdateAsync(id, dto);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message });
            }
            return Ok(new { response.StatusCode, response.Model, });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponseDto>> Delete(int id)
        {
            var resppnse = await _cartItemManager.DeleteAsync(id);
            if (!resppnse.IsSucceeded)
            {
                return NotFound(new { resppnse.StatusCode, resppnse.Message });
            }
            return Ok(new { resppnse.StatusCode, resppnse.Model, });
        }
    }
}
