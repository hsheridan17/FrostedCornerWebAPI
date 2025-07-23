using FrostedCornerWebAPI.Dtos.Supplies;

namespace FrostedCornerWebAPI.Services.SuppliesService
{
    public interface ISuppliesService
    {
        public Task<ServiceResponse<List<GetSuppliesOrderDto>>> GetAllSuppliesOrders();
    }
}
