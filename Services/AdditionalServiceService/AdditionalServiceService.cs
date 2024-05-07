using AutoMapper;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.AdditionalServiceService
{
    public class AdditionalServiceService : IAdditionalServiceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AdditionalServiceService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AdditionalServiceDto>> GetAditionalServicesAsync()
        {
            var additionalServices = await _context.AdditionalServices.ToListAsync();
            return _mapper.Map<List<AdditionalServiceDto>>(additionalServices);
        }

        public async Task<List<AdditionalServiceDto>> GetAditionalServicesByRestaurantAsync(Guid restaurantId)
        {
            var additionalServices = await _context.RestaurantAdditionalServices
                .Where(s => s.RestaurantId == restaurantId)
                .Select(s => s.AdditionalService).ToListAsync();

            return _mapper.Map<List<AdditionalServiceDto>>(additionalServices);
        }
    }
}
