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
                            .ThenInclude(fi=>fi.Item)
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
        public async Task<ServiceResponse<GetOrderDto>> GetOrdersByFranchiseId(int franchiseId)
        {
            var serviceResponse = new ServiceResponse<GetOrderDto>();

            try
            {
                var order = await _context.Orders
                       .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.FranchiseItem)
                    .FirstOrDefaultAsync(o => o.FranchiseId == franchiseId);

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

        public async Task<ServiceResponse<GetOrderDto>> GetOrderByCustomerId(int customerId)
        {
            var serviceResponse = new ServiceResponse<GetOrderDto>();

            try
            {
                var order = await _context.Orders
                       .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.FranchiseItem)
                    .FirstOrDefaultAsync(o => o.CustomerId == customerId);

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

        public async Task<ServiceResponse<GetOrderDto>> AddOrder(AddOrderDto order)
        {
            var serviceResponse = new ServiceResponse<List<GetOrderDto>>();

            try
            {
                var newOrder = _mapper.Map<Order>(order);

                // Need to await so there arent multiple threads
                if (newOrder.OrderItems is not null)
                {
                    // For each order item in the order, need to fetch the item using ItemId
                    foreach (var orderItem in newOrder.OrderItems)
                    {
                        // Get the franchise item
                        var franchiseItem = await _context.FranchiseItems
                            .Include(fi => fi.Item)
                            .FirstOrDefaultAsync(fi => fi.FranchiseItemId == orderItem.FranchiseItemId);

                        if (franchiseItem == null)
                        {
                            continue;
                        }

                        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == franchiseItem.ItemId);
                        if (item is not null)
                        {
                            // If item exists, set the order item properties
                            orderItem.FranchiseItem = franchiseItem;
                            orderItem.Order = newOrder;

                            if (franchiseItem.CustomPrice != null && franchiseItem.CustomPrice != 0)
                            {
                                // If custom price is set, use that
                                orderItem.SubTotal = franchiseItem.CustomPrice * orderItem.Quantity;
                            }
                            else
                            {
                                // Otherwise, use the item's price
                                orderItem.SubTotal = item.Price * orderItem.Quantity;
                            }

                            newOrder.Total += orderItem.SubTotal;
                        }
                    }
                }

                DateTime time = DateTime.Now;
                newOrder.Time = time;

                // Set delivery address
                newOrder.Address = order.Address;

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                var dbOrders = await _context.Orders
                    // Print order items too
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.FranchiseItem)
                    .ToListAsync();

                serviceResponse.Data = dbOrders
                    .Select(o => _mapper.Map<GetOrderDto>(o)).ToList();
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
