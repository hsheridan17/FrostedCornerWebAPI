namespace FrostedCornerWebAPI.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int FranchiseItemId { get; set; }
        public FranchiseItem FranchiseItem { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }

        public OrderItem()
        {
            this.OrderItemId = 0;
            this.OrderId = 0;
            this.ItemName = string.Empty;
            this.FranchiseItemId = 0;
            this.Quantity = 0;
            this.SubTotal = 0;
        }

    }
}
