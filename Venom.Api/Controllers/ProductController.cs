using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.Dtos;
using Venom.Application.Products;
using Venom.Application.Reviews.Dtos;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _productManager; // Corrected naming
        private readonly IMapper _mapper;

        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponseDto>> GetAll()
        {
            var response = await _productManager.GetAll();
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneralResponseDto>> GetById(int id)
        {
            var product = await _productManager.GetProductByIdAsync(id);
            if (product.IsSucceeded == false)
                return NotFound(new { product.Model, product.StatusCode });

            return Ok(new { product.Model, product.StatusCode });
        }

        [HttpGet("name/{productName}")] 
        public async Task<ActionResult<GeneralResponseDto>> GetByProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest(new { message = "Product name must be provided." });
            }

            var response = await _productManager.GetByProductNameAsync(productName);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpGet("category/{categoryName}")] 
        public async Task<ActionResult<GeneralResponseDto>> GetByCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest(new { message = "Category name must be provided." });
            }

            var response = await _productManager.GetByCategoryNameAsync(categoryName);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpGet("price/{price}")] 
        public async Task<ActionResult<GeneralResponseDto>> GetByPriceAsync(decimal price)
        {
            if (price < 0)
            {
                return BadRequest(new { message = "Price should be greater than or equal to zero." });
            }

            var response = await _productManager.GetByPriceAsync(price);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpGet("price/range/{highPrice}/{lowPrice}")]
        public async Task<ActionResult<GeneralResponseDto>> GetByPriceInRangeAsync(decimal highPrice, decimal lowPrice)
        {
            if (highPrice < 0 || lowPrice < 0)
            {
                return BadRequest(new { message = "High price and low price must be non-negative." });
            }

            var response = await _productManager.GetByPriceInRangeAsync(highPrice, lowPrice);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponseDto>> Add([FromBody] AddProductDto model)
        {
            var response = await _productManager.AddAsync(model);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GeneralResponseDto>> Update(int id, [FromBody] UpdateProductDto model)
        {
            var response = await _productManager.UpdateAsync(id, model);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GeneralResponseDto>> Delete(int id)
        {
            var response = await _productManager.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(new { response.StatusCode, response.Message }); // Return appropriate error code
            }
            return Ok(new { response.StatusCode, response.Model });
        }
    }

}
