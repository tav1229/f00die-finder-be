using f00die_finder_be.Dtos.ReviewComment;
using f00die_finder_be.Filters;
using f00die_finder_be.Services.ReviewComment;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.ReviewComment
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class ReviewCommentController : ControllerBase
    {
        private readonly IReviewCommentService _reviewCommentService;

        public ReviewCommentController(IReviewCommentService reviewCommentService)
        {
            _reviewCommentService = reviewCommentService;
        }

        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize = 10, int pageNumber = 1)
        {
            var result = await _reviewCommentService.GetReviewCommentsOfRestaurantAsync(restaurantId, pageSize, pageNumber);
            return Ok(result);
        }

        [HttpPost("restaurant")]
        public async Task<IActionResult> AddAsync([FromBody] ReviewCommentAddDto reviewCommentAddDto)
        {
            var result = await _reviewCommentService.AddAsync(reviewCommentAddDto);
            return Ok(result);
        }
    }
}
