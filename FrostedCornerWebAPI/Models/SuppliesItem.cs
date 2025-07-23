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

        public SuppliesItem()
        {
            this.SuppliesItemId = 0;
            this.OrderId = 0;
            this.ItemId = 0;
            this.Quantity = 0;
            this.SubTotal = 0;
        }
    }
}
