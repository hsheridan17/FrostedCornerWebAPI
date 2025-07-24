using FrostedCornerWebAPI.Dtos.Order;

namespace FrostedCornerWebAPI.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<GetOrderDto>>> GetAllOrders();
        Task<ServiceResponse<GetOrderDto>> GetOrderById(int id);
        Task<ServiceResponse<List<GetOrderDto>>> GetOrdersByCustomerId(int customerId);
        Task<ServiceResponse<List<GetOrderDto>>> GetOrdersByFranchiseId(int franchiseId);
        Task<ServiceResponse<GetOrderDto>> AddOrder(AddOrderDto order);

    }
}
