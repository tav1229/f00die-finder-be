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

            await _cacheService.RemoveAsync($"reviewComments-restaurant-{review.RestaurantId}");
            return review.Id;
        }

        public async Task<PagedResult<ReviewCommentDto>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber)
        {
            var reviews = await _cacheService.GetOrCreateAsync($"reviewComments-restaurant-{restaurantId}", async () =>
            {
                var reviewQuery = await _unitOfWork.GetAllAsync<Entities.ReviewComment>();
                return await reviewQuery
                    .Include(r => r.User)
                    .Where(r => r.RestaurantId == restaurantId)
                    .ToListAsync();
            });

            int totalItems = reviews.Count();
            var items = reviews
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => _mapper.Map<ReviewCommentDto>(r))
                .ToList();

            return new PagedResult<ReviewCommentDto>
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }
    }
}
