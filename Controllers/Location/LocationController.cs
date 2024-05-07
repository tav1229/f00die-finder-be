using f00die_finder_be.Filters;
using f00die_finder_be.Services.Location;
using Microsoft.AspNetCore.Mvc;

namespace f00die_finder_be.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeFilter]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("province-or-city")]
        public async Task<IActionResult> GetProvinceOrCitysAsync()
        {
            var result = await _locationService.GetProvinceOrCitysAsync();
            return Ok(result);
        }

        [HttpGet("district/{provinceOrCityId}")]    
        public async Task<IActionResult> GetDistrictsByProvinceOrCityAsync(Guid provinceOrCityId)
        {
            var result = await _locationService.GetDistrictsByProvinceOrCityAsync(provinceOrCityId);
            return Ok(result);
        }

        [HttpGet("ward-or-commune/{districtId}")]
        public async Task<IActionResult> GetWardOrCommunesByDistrictAsync(Guid districtId)
        {
            var result = await _locationService.GetWardOrCommunesByDistrictAsync(districtId);
            return Ok(result);
        }
    }
}
