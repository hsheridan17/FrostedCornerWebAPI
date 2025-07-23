using FrostedCornerWebAPI.Dtos.FranchiseItem;
using FrostedCornerWebAPI.Services.FranchiseItemService;
using FrostedCornerWebAPI.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace FrostedCornerWebAPI.Controllers
{
    [ApiController]
    [Route("api/Franchise")]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseItemService _franchiseItemService;

        public FranchiseController(IFranchiseItemService franchiseItemService)
        {
            _franchiseItemService = franchiseItemService;
        }

        // GET ALL FRANCHISES
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetFranchiseDto>>>> GetAllFranchises()
        {
            return Ok(await _franchiseItemService.GetAllFranchises());
        }

        [HttpPost("addFranchise")]
        public async Task<ActionResult<ServiceResponse<List<GetFranchiseDto>>>> AddFranchise(AddFranchiseDto franchise)
        {
            return Ok(await _franchiseItemService.AddFranchise(franchise));
        }

        [HttpPost("addItem/{franchiseId}/{itemId}")]
        public async Task<ActionResult<ServiceResponse<GetFranchiseDto>>> AddFranchiseItem(int franchiseId, int itemId)
        {
            return Ok(await _franchiseItemService.AddFranchiseItem(franchiseId, itemId));
        }

        [HttpPost("addToAllFranchises/{itemId}")]
        public async Task<ActionResult<ServiceResponse<List<GetFranchiseDto>>>> AddItemToAllFranchises(int itemId)
        {
            return Ok(await _franchiseItemService.AddItemToAllFranchises(itemId));
        }


        [HttpPost("editItem")]
        public async Task<ActionResult<ServiceResponse<GetFranchiseItemDto>>> EditFranchiseItem(EditFranchiseItemDto updatedFranchiseItem)
        {
            return Ok(await _franchiseItemService.EditFranchiseItem(updatedFranchiseItem));
        }

        [HttpGet("{franchiseId}")]
        public async Task<ActionResult<ServiceResponse<GetFranchiseDto>>> GetFranchiseById(int franchiseId)
        {
            return Ok(await _franchiseItemService.GetFranchiseById(franchiseId));
        }
    }
}
