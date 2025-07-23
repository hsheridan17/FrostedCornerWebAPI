using FrostedCornerWebAPI.Dtos.OrderItem;

namespace FrostedCornerWebAPI.Dtos.Order
{
    public class AddOrderDto
    {
        public int FranchiseId { get; set; }
        public List<AddOrderItemDto>? OrderItems { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
