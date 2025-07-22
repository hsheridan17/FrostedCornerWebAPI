using AutoMapper;
using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Dtos.Order;
using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        private static List<Order> orders = new List<Order>();

        public OrderService()
        {

        }
        public OrderService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders()
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();
            try
            {
                var dbOrders = await _context.Orders
                    // This will make sure that OrderItems and their
                    // associated Items are displayed as well
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.FranchiseItem)
                    .ToListAsync();

                serviceResponse.Data = dbOrders.Select(order => _mapper.Map<GetOrderDto>(order)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }

        // Gets a specific order given an id 
        public async Task<ServiceResponse<GetOrderDto>> GetOrderById(int id)
        {
            var serviceResponse = new ServiceResponse<GetOrderDto>();

            try
            {
                var order = await _context.Orders
                       .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.FranchiseItem)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                // Check if the given order exists
                if (order == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found.";
                    return serviceResponse;
                }

                // Order exists, map to dto
                serviceResponse.Data = _mapper.Map<GetOrderDto>(order);
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
