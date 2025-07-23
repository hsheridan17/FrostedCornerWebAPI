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

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<ServiceResponse<GetOrderDto>>> GetOrderByCustomerId(int customerId)
        {
            return Ok(await _orderService.GetOrderByCustomerId(customerId));
        }

        [HttpGet("franchise/{franchiseId}")]
        public async Task<ActionResult<ServiceResponse<GetOrderDto>>> GetOrdersByFranchiseId(int franchiseId)
        {
            return Ok(await _orderService.GetOrdersByFranchiseId(franchiseId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetOrderDto>>> AddOrder(AddOrderDto order)
        {
            return Ok(await _orderService.AddOrder(order));
        }
    }
}
