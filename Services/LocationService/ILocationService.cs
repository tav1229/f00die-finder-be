using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Services.Location
{
    public interface ILocationService
    {
        Task<CustomResponse<List<ProvinceOrCityDto>>> GetProvinceOrCitysAsync();
        Task<CustomResponse<List<DistrictDto>>> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId);
        Task<CustomResponse<List<WardOrCommuneDto>>> GetWardOrCommunesByDistrictAsync(Guid districtId);
    }
}
