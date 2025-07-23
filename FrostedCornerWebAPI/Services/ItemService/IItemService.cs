using FrostedCornerWebAPI.Dtos.FranchiseItem;
using FrostedCornerWebAPI.Dtos.Item;

namespace FrostedCornerWebAPI.Services.ItemService
{
    public interface IItemService
    {
        public Task<ServiceResponse<List<GetItemDto>>> GetAllItems();

        public Task<ServiceResponse<GetItemDto>> GetItemById(int id);

        public Task<ServiceResponse<List<GetItemDto>>> AddItem(AddItemDto item);

        public Task<ServiceResponse<List<GetItemDto>>> RemoveItemById(long id);

        public Task<ServiceResponse<List<GetItemDto>>> GetItemsByType(ItemType type);
    }
}
