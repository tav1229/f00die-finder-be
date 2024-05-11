using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.ReviewComment;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReviewComment
{
    public class ReviewCommentService : BaseService, IReviewCommentService
    {
        public ReviewCommentService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<Guid> AddAsync(ReviewCommentAddDto reviewCommentAddDto)
        {
            var review = _mapper.Map<Entities.ReviewComment>(reviewCommentAddDto);
            review.UserId = _currentUserService.UserId;

            await _unitOfWork.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveByPrefixAsync("reviewComments");
            return review.Id;
        }

        public async Task<PagedResult<ReviewCommentDto>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber)
        {
            return await _cacheService.GetOrCreateAsync($"reviewComments-restaurant-{restaurantId}", async () =>
            {
                var reviewQuery = await _unitOfWork.GetAllAsync<Entities.ReviewComment>();
                var reviews = reviewQuery
                    .Include(r => r.User)
                    .Where(r => r.RestaurantId == restaurantId);

                return new PagedResult<ReviewCommentDto>
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(await reviews.CountAsync() / (double)pageSize),
                    Items = await reviews
                        .OrderByDescending(r => r.CreatedDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Select(r => _mapper.Map<ReviewCommentDto>(r))
                        .ToListAsync(),
                };
            });
        }
    }
}
