using FrostedCornerWebAPI.Dtos.Order;
using FrostedCornerWebAPI.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace FrostedCornerWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor that receives an IOrderService implementation
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetOrderDto>>>> GetOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetOrderDto>>> GetOrderById(int id)
        {
            return Ok(await _orderService.GetOrderById(id));
        }
    }
}
