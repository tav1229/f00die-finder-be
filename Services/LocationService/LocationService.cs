using AutoMapper;
using f00die_finder_be.Data;
using f00die_finder_be.Dtos.Location;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LocationService(DataContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DistrictDto>> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId)
        {
            var districts = await _context.Districts
                .Where(d => d.ProvinceOrCityId == provinceOrCityId).ToListAsync();
            return _mapper.Map<List<DistrictDto>>(districts);
        }

        public Task<List<ProvinceOrCityDto>> GetProvinceOrCitysAsync()
        {
            var provinceOrCitys = _context.ProvinceOrCities.ToList();
            return Task.FromResult(_mapper.Map<List<ProvinceOrCityDto>>(provinceOrCitys));
        }

        public Task<List<WardOrCommuneDto>> GetWardOrCommunesByDistrictAsync(Guid districtId)
        {
            var wardOrCommunes = _context.WardOrCommunes
                .Where(w => w.DistrictId == districtId).ToList();
            return Task.FromResult(_mapper.Map<List<WardOrCommuneDto>>(wardOrCommunes));
        }
    }
}
