namespace FrostedCornerWebAPI.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int FranchiseItemId { get; set; }
        public FranchiseItem FranchiseItem { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }

    }
}
