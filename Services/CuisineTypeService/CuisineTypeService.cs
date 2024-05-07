using AutoMapper;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.CuisineTypeService
{
    public class CuisineTypeService : ICuisineTypeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CuisineTypeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CuisineTypeDto>> GetCuisineTypesAsync()
        {
            var cuisineTypes = await _context.CuisineTypes.ToListAsync();
            return _mapper.Map<List<CuisineTypeDto>>(cuisineTypes);
        }

        public async Task<List<CuisineTypeDto>> GetCuisineTypesByRestaurantAsync(Guid restaurantId)
        {
            var cuisineTypes = await _context.RestaurantCuisineTypes
                .Where(s => s.RestaurantId == restaurantId)
                .Select(s => s.CuisineType).ToListAsync();

            return _mapper.Map<List<CuisineTypeDto>>(cuisineTypes);
        }
    }
}
