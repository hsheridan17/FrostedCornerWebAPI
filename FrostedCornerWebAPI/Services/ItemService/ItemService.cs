using AutoMapper;
using FrostedCornerWebAPI.Data;
using FrostedCornerWebAPI.Dtos.Item;
using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Services.ItemService
{
    // Ensure there is only one definition of ItemService in this namespace  
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ItemService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetAllItems()
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            var dbItems = await _context.Items.ToListAsync();
            serviceResponse.Data = dbItems.Select(item => _mapper.Map<GetItemDto>(item)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDto>> GetItemById(int id)
        {
            var serviceResponse = new ServiceResponse<GetItemDto>();
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Item not found.";
                return serviceResponse;
            }
            serviceResponse.Data = _mapper.Map<GetItemDto>(item);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto itemDto)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            var newItem = _mapper.Map<Item>(itemDto);

            // Handle dietary restrictions
            newItem.DietaryRestrictions = new List<DietaryRestriction>();
            foreach (var drDto in itemDto.DietaryRestrictions)
            {
                var dr = await _context.DietaryRestrictions
                    .FirstOrDefaultAsync(d => d.DietaryRestrictionId == drDto.DietaryRestrictionId);
                if (dr != null)
                {
                    newItem.DietaryRestrictions.Add(dr);
                }
            }
            
            // Add new item
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            var dbItems = await _context.Items.ToListAsync();
            serviceResponse.Data = dbItems.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> RemoveItemById(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            var dbItem = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
            if (dbItem == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Item not found.";
                return serviceResponse;
            }
            _context.Items.Remove(dbItem);
            await _context.SaveChangesAsync();

            var dbItems = await _context.Items.ToListAsync();
            serviceResponse.Data = dbItems.Select(i => _mapper.Map<GetItemDto>(i)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDto>>> GetItemsByType(ItemType type)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDto>>();
            var dbItems = await _context.Items.Where(i=>i.ItemType == type).ToListAsync();
            serviceResponse.Data = dbItems.Select(item => _mapper.Map<GetItemDto>(item)).ToList();
            return serviceResponse;
        }
    }
}
