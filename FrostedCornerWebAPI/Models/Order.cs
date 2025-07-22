namespace FrostedCornerWebAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderType OrderType { get; set; }
        public string? Address { get; set; } = string.Empty;
        public float Total { get; set; }
        public DateTime Time { get; set; }

        public Order()
        {
            this.OrderId = 0;
            this.CustomerId = 0;
            this.OrderItems = new List<OrderItem>();
            this.OrderType = OrderType.Pickup;
            this.Total = 0.0f;
            this.Time = DateTime.Now;
        }
    }

}
