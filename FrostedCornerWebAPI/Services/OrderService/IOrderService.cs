using FrostedCornerWebAPI.Dtos.Order;

namespace FrostedCornerWebAPI.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);
        Task<ServiceResponse<GetOrderDto>> GetOrderByCustomerId(int customerId);
        Task<ServiceResponse<GetOrderDto>> GetOrdersByFranchiseId(int franchiseId);
        Task<ServiceResponse<GetOrderDto>> AddOrder(AddOrderDto order);

    }
}
