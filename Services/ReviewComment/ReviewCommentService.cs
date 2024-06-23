using AutoMapper.QueryableExtensions;
using f00die_finder_be.Data.Entities;
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

        public async Task<CustomResponse<ReviewCommentDto>> AddAsync(ReviewCommentAddDto reviewCommentAddDto)
        {
            var review = _mapper.Map<Data.Entities.ReviewComment>(reviewCommentAddDto);
            review.UserId = _currentUserService.UserId;

            var restaurant = await (await _unitOfWork.GetQueryableAsync<Restaurant>())
                .FirstOrDefaultAsync(r => r.Id == reviewCommentAddDto.RestaurantId);

            var reviewQuery = await _unitOfWork.GetQueryableAsync<Data.Entities.ReviewComment>();
            var reviews = await reviewQuery
                .Where(r => r.RestaurantId == restaurant.Id)
                .ToListAsync();

            restaurant.Rating = (short)Math.Round(reviews.Average(r => r.Rating));

            await _unitOfWork.UpdateAsync(restaurant);

            await _unitOfWork.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync($"reviewComments-restaurant-{review.RestaurantId}");
            
            var data = await reviewQuery
                .Where(r => r.Id == review.Id)
                .ProjectTo<ReviewCommentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return new CustomResponse<ReviewCommentDto>
            {
                Data = _mapper.Map<ReviewCommentDto>(review)
            };
        }

        public async Task<CustomResponse<List<ReviewCommentDto>>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber)
        {
            var reviews = await _cacheService.GetOrCreateAsync($"reviewComments-restaurant-{restaurantId}", async () =>
            {
                var reviewQuery = await _unitOfWork.GetQueryableAsync<Data.Entities.ReviewComment>();
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

            return new CustomResponse<List<ReviewCommentDto>>
            {
                Data = items,
                Meta = new MetaData
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                    TotalCount = totalItems
                }
            };
        }
    }
}
