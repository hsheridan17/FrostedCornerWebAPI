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
        public async Task<ServiceResponse<List<GetSuppliesOrderDto>>> AddSuppliesOrder(AddSuppliesOrderDto order)
        {
            var serviceResponse = new ServiceResponse<List<GetSuppliesOrderDto>>();

            try
            {
                var newOrder = _mapper.Map<SuppliesOrder>(order);

                if (newOrder.SuppliesItems is not null)
                {
                    // For each order item in the order, need to fetch the item using ItemId
                    foreach (var suppliesItem in newOrder.SuppliesItems)
                    {
                        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == suppliesItem.ItemId);
                        if (item is not null)
                        {
                            // If item exists, set the order item properties
                            suppliesItem.Item = item;
                            suppliesItem.SubTotal = item.Price * suppliesItem.Quantity;
                            newOrder.Total += suppliesItem.SubTotal;
                        }
                    }
                }

                DateTime time = DateTime.Now;
                newOrder.Time = time;

                _context.SuppliesOrders.Add(newOrder);
                await _context.SaveChangesAsync();

                var dbOrders = await _context.SuppliesOrders
                    // Be sure to print order items too
                    .Include(o => o.SuppliesItems)
                        .ThenInclude(oi => oi.Item)
                    .ToListAsync();

                serviceResponse.Data = dbOrders.Select(o => _mapper.Map<GetSuppliesOrderDto>(o)).ToList();
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