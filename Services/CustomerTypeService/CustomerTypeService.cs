using f00die_finder_be.Data.Entities;
using f00die_finder_be.Dtos;
using f00die_finder_be.Dtos.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Services.CustomerTypeService
{
    public class CustomerTypeService : BaseService, ICustomerTypeService
    {
        public CustomerTypeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<CustomResponse<List<CustomerTypeDto>>> GetCustomerTypesAsync()
        {
            var data = await _cacheService.GetOrCreateAsync("customerTypes", async () =>
            {
                var customerTypeQuery = await _unitOfWork.GetQueryableAsync<CustomerType>();
                var customerTypes = await customerTypeQuery.ToListAsync();

                return _mapper.Map<List<CustomerTypeDto>>(customerTypes);
            });

            return new CustomResponse<List<CustomerTypeDto>>
            {
                Data = data
            };

        }
    }
}
