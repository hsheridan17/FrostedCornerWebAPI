using FrostedCornerWebAPI.Dtos.Supplies;

namespace FrostedCornerWebAPI.Services.SuppliesService
{
    public interface ISuppliesService
    {
        Task<ServiceResponse<List<GetSuppliesOrderDto>>> GetAllSuppliesOrders();
        Task<ServiceResponse<List<GetSuppliesOrderDto>>> AddSuppliesOrder(AddSuppliesOrderDto order);
    }
}
