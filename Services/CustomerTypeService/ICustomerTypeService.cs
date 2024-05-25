using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;

namespace f00die_finder_be.Services.CustomerTypeService
{
    public interface ICustomerTypeService
    {
        Task<CustomResponse<List<CustomerTypeDto>>> GetCustomerTypesAsync();
    }
}
