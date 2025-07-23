using FrostedCornerWebAPI.Dtos.Item;
namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class GetSuppliesItemDto
    {
        public int SuppliesItemId { get; set; }
        public int OrderId { get; set; }
        public GetItemDto Item { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }
    }
}
