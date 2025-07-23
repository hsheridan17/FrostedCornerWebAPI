using AutoMapper;
using AutoMapper;
using FrostedCornerWebAPI.Dtos.FranchiseItem;
using FrostedCornerWebAPI.Dtos.Item;
using FrostedCornerWebAPI.Dtos.Order;
using FrostedCornerWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Services.FranchiseItemService
{
    public class FranchiseItemService : IFranchiseItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
       
        public FranchiseItemService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetFranchiseDto>>> GetAllFranchises()
        {
            var serviceResponse = new ServiceResponse<List<GetFranchiseDto>>();

            try
            {
                var dbFranchises = await _context.Franchises
                    .Include(f => f.FranchiseItems)
                        .ThenInclude(i => i.Item)
                        .ThenInclude(d=>d.DietaryRestrictions)
                    .ToListAsync();

                serviceResponse.Data = dbFranchises.Select(franchise => _mapper.Map<GetFranchiseDto>(franchise)).ToList();
            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFranchiseDto>> GetFranchiseById(int franchiseId)
        {
            var serviceResponse = new ServiceResponse<GetFranchiseDto>();

            try
            {
                var franchise = await _context.Franchises
                    .Include(fi=>fi.FranchiseItems)
                        .ThenInclude(i=>i.Item)
                    .FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

                if (franchise == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Item not found.";
                    return serviceResponse;
                }

                serviceResponse.Data = _mapper.Map<GetFranchiseDto>(franchise);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFranchiseDto>>> AddFranchise(AddFranchiseDto franchise)
        {
            var serviceResponse = new ServiceResponse<List<GetFranchiseDto>>();

            try
            {
                var newFranchise = _mapper.Map<Franchise>(franchise);
                _context.Franchises.Add(newFranchise);
                await _context.SaveChangesAsync();

                var menu = await _context.Items.ToListAsync();
                List<FranchiseItem> franchiseItems = new List<FranchiseItem>();
                foreach (var item in menu)
                {
                    if (item.ItemType == ItemType.Food)
                    {
                        FranchiseItem franchiseItem = new FranchiseItem
                        {
                            FranchiseId = newFranchise.FranchiseId,
                            Item = item,
                            ItemId = item.ItemId
                        };

                        franchiseItems.Add(franchiseItem);
                    }
                }

                _context.FranchiseItems.AddRange(franchiseItems);
                await _context.SaveChangesAsync();


                var dbFranchises = await _context.Franchises.ToListAsync();
                serviceResponse.Data = serviceResponse.Data = _mapper.Map<List<GetFranchiseDto>>(dbFranchises);

            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFranchiseDto>> AddFranchiseItem(int franchiseId, int itemId)
        {
            var serviceResponse = new ServiceResponse<GetFranchiseDto>();

            try
            {
                var franchise = await _context.Franchises
                    .Include(f => f.FranchiseItems)
                        .ThenInclude(i => i.Item)
                    .FirstOrDefaultAsync(fi => fi.FranchiseId == franchiseId);

                if (franchise is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Order not found.";
                    return serviceResponse;
                }

                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.ItemId == itemId);

                if (item is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Item not found.";
                    return serviceResponse;
                }

                FranchiseItem franchiseItem = new FranchiseItem
                {
                    ItemId = itemId,
                    Item = item,
                    FranchiseId = franchiseId
                };

                franchise.FranchiseItems!.Add(franchiseItem);

                await _context.SaveChangesAsync();
                var dbFranchises = await _context.Franchises.ToListAsync();
                serviceResponse.Data = _mapper.Map<GetFranchiseDto>(franchise);


            } catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFranchiseDto>>> AddItemToAllFranchises(int itemId)
        {
            var serviceResponse = new ServiceResponse<List<GetFranchiseDto>>();

            try
            {
                var item = await _context.Items
                    .FirstOrDefaultAsync(i => i.ItemId == itemId);

                // Make sure item exists and is a food item
                if (item is null || item.ItemType == ItemType.Supply)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Food item with that Id not found.";
                    return serviceResponse;
                }

                var franchises = await _context.Franchises.ToListAsync();
                foreach (var franchise in franchises)
                {
                    FranchiseItem franchiseItem = new FranchiseItem
                    {
                        ItemId = itemId,
                        Item = item,
                        FranchiseId = franchise.FranchiseId
                    };

                    franchise.FranchiseItems!.Add(franchiseItem);
                }

                await _context.SaveChangesAsync();
                var dbFranchises = await _context.Franchises.ToListAsync();
                serviceResponse.Data = _mapper.Map<List<GetFranchiseDto>>(dbFranchises);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFranchiseItemDto>> EditFranchiseItem(EditFranchiseItemDto updatedFranchiseItem)
        {
            var serviceResponse = new ServiceResponse<GetFranchiseItemDto>();

            var item = await _context.FranchiseItems.FirstOrDefaultAsync(i => i.ItemId == updatedFranchiseItem.ItemId
                                                                         && i.FranchiseId == updatedFranchiseItem.FranchiseId);
            if (item == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Item not found.";
                return serviceResponse;
            }

            if (updatedFranchiseItem.CustomColor is not null)
            {
                item.CustomColor = updatedFranchiseItem.CustomColor;
            }

            if (updatedFranchiseItem.CustomPrice != null)
            {
                item.CustomPrice = updatedFranchiseItem.CustomPrice;
            }

            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetFranchiseItemDto>(item);

            return serviceResponse;

        }
    }
}
