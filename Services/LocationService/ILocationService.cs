using f00die_finder_be.Dtos.Location;

namespace f00die_finder_be.Services.Location
{
    public interface ILocationService
    {
        Task<List<ProvinceOrCityDto>> GetProvinceOrCitysAsync();
        Task<List<DistrictDto>> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId);
        Task<List<WardOrCommuneDto>> GetWardOrCommunesByDistrictAsync(Guid districtId);
    }
}
