using FrostedCornerWebAPI.Dtos.Supplies;
using FrostedCornerWebAPI.Services.SuppliesService;
using Microsoft.AspNetCore.Mvc;

namespace FrostedCornerWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliesController : ControllerBase
    {
        private readonly ISuppliesService _suppliesService;

        public SuppliesController(ISuppliesService suppliesService)
        {
            _suppliesService = suppliesService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSuppliesOrderDto>>>> GetAllSuppliesOrders()
        {
            return Ok(await _suppliesService.GetAllSuppliesOrders());
        }
    }
}
