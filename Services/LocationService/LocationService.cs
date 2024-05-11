using f00die_finder_be.Dtos.Location;
using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.Location
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<List<DistrictDto>> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId)
        {
            return await _cacheService.GetOrCreateAsync($"districts-provinceOrCity-{provinceOrCityId}", async () =>
            {
                var districtQuery = await _unitOfWork.GetAllAsync<District>();
                var districts = await districtQuery
                    .Where(d => d.ProvinceOrCityId == provinceOrCityId).ToListAsync();
                return _mapper.Map<List<DistrictDto>>(districts);
            });
        }

        public async Task<List<ProvinceOrCityDto>> GetProvinceOrCitysAsync()
        {
            return await _cacheService.GetOrCreateAsync("provinceOrCitys", async () =>
            {
                var provinceOrCityQuery = await _unitOfWork.GetAllAsync<ProvinceOrCity>();
                var provinceOrCitys = await provinceOrCityQuery.ToListAsync();
                return _mapper.Map<List<ProvinceOrCityDto>>(provinceOrCitys);
            });
        }

        public async Task<List<WardOrCommuneDto>> GetWardOrCommunesByDistrictAsync(Guid districtId)
        {
            return await _cacheService.GetOrCreateAsync($"wardOrCommunes-district-{districtId}", async () =>
            {
                var wardOrCommuneQuery = await _unitOfWork.GetAllAsync<WardOrCommune>();
                var wardOrCommunes = await wardOrCommuneQuery
                    .Where(w => w.DistrictId == districtId).ToListAsync();
                return _mapper.Map<List<WardOrCommuneDto>>(wardOrCommunes);
            });
        }
    }
}
