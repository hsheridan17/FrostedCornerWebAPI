using FrostedCornerWebAPI.Dtos.Item;

namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class AddSuppliesItemDto
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }
    }
}
