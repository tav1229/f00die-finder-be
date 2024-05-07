using AutoMapper;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.ServingTypeService
{
    public class ServingTypeService : IServingTypeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ServingTypeService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ServingTypeDto>> GetServingTypesAsync()
        {
            var servingTypes = await _context.ServingTypes.ToListAsync();
            return _mapper.Map<List<ServingTypeDto>>(servingTypes);
        }

        public async Task<List<ServingTypeDto>> GetServingTypesByRestaurantAsync(Guid restaurantId)
        {
            var servingTypes = await _context.RestaurantServingTypes
                .Where(s => s.RestaurantId == restaurantId)
                .Select(s => s.ServingType).ToListAsync();

            return _mapper.Map<List<ServingTypeDto>>(servingTypes);
        }
    }
}
