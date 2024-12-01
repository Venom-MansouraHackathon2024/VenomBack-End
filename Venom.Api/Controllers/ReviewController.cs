using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Venom.Application.Reviews.Dtos;
using Venom.Application.Reviews;
using Venom.Application.Dtos;

namespace Venom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManager _reviewManager;

        public ReviewController(IReviewManager reviewManager)
        {
            _reviewManager = reviewManager;
        }

        [HttpGet]
        public async Task<ActionResult<GeneralResponseDto>> GetAllAsync()
        {
            var result = await _reviewManager.GetAll();
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message }); 
            }
            return Ok(result);
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<GeneralResponseDto>> GetByIdAsync(int id)
        {
            var result = await _reviewManager.GetByIdAsync(id);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(result);
        }
        [HttpGet("user/{userId}")] // Changed for clarity and to avoid conflicts
        public async Task<ActionResult<GeneralResponseDto>> GetByUserIdAsync(string userId)
        {
            var result = await _reviewManager.GetByUserIdAsync(userId);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDto dto)
        {
            var result = await _reviewManager.AddAsync(dto);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message });
            }

            return Ok(result.Message);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateReview(int id ,UpdateReviewDto dto)
        {
            var reviewExists = await _reviewManager.GetByIdAsync(id);
            if (!reviewExists.IsSucceeded)
            {
                return NotFound(new { reviewExists.StatusCode, reviewExists.Message });
            }

            var result = await _reviewManager.UpdateAsync(id, dto);
            if (!result.IsSucceeded)
            {
                return NotFound( new { result.StatusCode, result.Message }); // Return appropriate error code
            }

            return Ok(result);
        }
        [HttpGet("{productId}/average-rating")]
        public async Task<IActionResult> GetAverageRating(int productId)
        {
            var rating = await _reviewManager.GetAverageRatingAsync(productId);
            return Ok(rating);
        }

        [HttpGet("{productId}/reviews")]
        public async Task<IActionResult> GetProductReviews(int productId)
        {
            var reviews = await _reviewManager.GetProductReviewsAsync(productId);
            return Ok(reviews);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GeneralResponseDto>> DeleteAsync(int id)
        {
            var result = await _reviewManager.DeleteAsync(id);
            if (!result.IsSucceeded)
            {
                return NotFound(new { result.StatusCode, result.Message }); // Return appropriate error code
            }
            return Ok(result);
        }


    }
}
