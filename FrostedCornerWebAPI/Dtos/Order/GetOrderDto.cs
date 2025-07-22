namespace FrostedCornerWebAPI.Dtos.Order
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public List<GetOrderDto> OrderItems { get; set; }
        public float Total { get; set; }   
        public OrderType OrderType { get; set; }
        public string? Address { get; set; } = string.Empty;
        public DateTime Time { get; set; }

    }
}
