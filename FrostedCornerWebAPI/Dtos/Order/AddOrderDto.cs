using FrostedCornerWebAPI.Dtos.OrderItem;

namespace FrostedCornerWebAPI.Dtos.Order
{
    public class AddOrderDto
    {
        public List<AddOrderItemDto>? OrderItems { get; set; }
    }
}
