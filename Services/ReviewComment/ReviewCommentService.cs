using AutoMapper;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.ReviewComment;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ReviewComment
{
    public class ReviewCommentService : IReviewCommentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReviewCommentService(DataContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> AddAsync(ReviewCommentAddDto reviewCommentAddDto)
        {
            var review = _mapper.Map<Models.ReviewComment>(reviewCommentAddDto);
            review.UserId = _currentUserService.UserId;

            _context.ReviewComments.Add(review);
            await _context.SaveChangesAsync();
            return review.Id;
        }

        public async Task<PagedResult<ReviewCommentDto>> GetReviewCommentsOfRestaurantAsync(Guid restaurantId, int pageSize, int pageNumber)
        {
            var reviews = _context.ReviewComments
                .Where(r => r.RestaurantId == restaurantId)
                .OrderByDescending(r => r.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var listReviews = await reviews.ToListAsync();

            return new PagedResult<ReviewCommentDto>
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(await _context.ReviewComments.CountAsync() / (double)pageSize),
                Items = _mapper.Map<List<ReviewCommentDto>>(listReviews),
            };
        }
    }
}
