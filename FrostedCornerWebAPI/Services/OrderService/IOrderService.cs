using FrostedCornerWebAPI.Dtos.Order;

namespace FrostedCornerWebAPI.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);

    }
}
