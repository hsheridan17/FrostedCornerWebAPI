using AutoMapper;
using FrostedCornerWebAPI.Dtos.Order;
using FrostedCornerWebAPI.Dtos.Supplies;
using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Services.SuppliesService
{
    public class SuppliesService : ISuppliesService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public SuppliesService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetSuppliesOrderDto>>> GetAllSuppliesOrders()
        {
            var serviceResponse = new ServiceResponse<List<GetSuppliesOrderDto>>();
            try
            {
                var dbSuppliesOrders = await _context.SuppliesOrders
                    // This will make sure that OrderItems and their
                    // associated Items are displayed as well
                    .Include(si => si.SuppliesItems)
                        .ThenInclude(i => i.Item)
                    .ToListAsync();

                serviceResponse.Data = dbSuppliesOrders.Select(order => _mapper.Map<GetSuppliesOrderDto>(order)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }


    }
}
