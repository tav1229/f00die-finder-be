using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.ReviewComment;

namespace f00die_finder_be.Services.ReviewComment
{
    public interface IReviewCommentService
    {
        Task<CustomResponse<List<ReviewCommentDto>>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber);
        Task<CustomResponse<ReviewCommentDto>> AddAsync(ReviewCommentAddDto reviewCommentAddDto);
    }
}
