using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Location;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.Location
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<CustomResponse<List<DistrictDto>>> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId)
        {
            var data = await _cacheService.GetOrCreateAsync($"districts-provinceOrCity-{provinceOrCityId}", async () =>
            {
                var districtQuery = await _unitOfWork.GetQueryableAsync<District>();
                var districts = await districtQuery
                    .Where(d => d.ProvinceOrCityId == provinceOrCityId).ToListAsync();
                return _mapper.Map<List<DistrictDto>>(districts);
            });

            return new CustomResponse<List<DistrictDto>>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<ProvinceOrCityDto>>> GetProvinceOrCitysAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("provinceOrCitys", async () =>
            {
                var provinceOrCityQuery = await _unitOfWork.GetQueryableAsync<ProvinceOrCity>();
                var provinceOrCitys = await provinceOrCityQuery.ToListAsync();
                return _mapper.Map<List<ProvinceOrCityDto>>(provinceOrCitys);
            });

            return new CustomResponse<List<ProvinceOrCityDto>>
            {
                Data = data
            };
        }

        public async Task<CustomResponse<List<WardOrCommuneDto>>> GetWardOrCommunesByDistrictAsync(Guid districtId)
        {
            var data = await _cacheService.GetOrCreateAsync($"wardOrCommunes-district-{districtId}", async () =>
            {
                var wardOrCommuneQuery = await _unitOfWork.GetQueryableAsync<WardOrCommune>();
                var wardOrCommunes = await wardOrCommuneQuery
                    .Where(w => w.DistrictId == districtId).ToListAsync();
                return _mapper.Map<List<WardOrCommuneDto>>(wardOrCommunes);
            });

            return new CustomResponse<List<WardOrCommuneDto>>
            {
                Data = data
            };
        }
    }
}
