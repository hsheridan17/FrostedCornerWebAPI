namespace FrostedCornerWebAPI.Models
{
    public class SuppliesItem
    {
        public int SuppliesItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }

    }
}
