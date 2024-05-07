using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.ReviewComment;

namespace f00die_finder_be.Services.ReviewComment
{
    public interface IReviewCommentService
    {
        Task<PagedResult<ReviewCommentDto>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber);
        Task<Guid> AddAsync(ReviewCommentAddDto reviewCommentAddDto);
    }
}
