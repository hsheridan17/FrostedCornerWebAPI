using FrostedCornerWebAPI.Dtos.OrderItem;

namespace FrostedCornerWebAPI.Dtos.Order
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public int FranchiseId { get; set; }
        public int CustomerId { get; set; }
        public List<GetOrderItemDto> OrderItems { get; set; }
        public float Total { get; set; }   
        public OrderType OrderType { get; set; }
        public string? Address { get; set; } = string.Empty;
        public DateTime Time { get; set; }

    }
}
