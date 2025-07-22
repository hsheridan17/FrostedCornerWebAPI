using FrostedCornerWebAPI.Dtos.Item;
using FrostedCornerWebAPI.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace FrostedCornerWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> GetAllItems()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetItemDto>>> GetItemById(int id)
        {
            return Ok(await _itemService.GetItemById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> AddItem(AddItemDto item)
        {
            return Ok(await _itemService.AddItem(item));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDto>>>> RemoveItem(long id)
        {
            return Ok(await _itemService.RemoveItemById(id));
        }
    }
}
