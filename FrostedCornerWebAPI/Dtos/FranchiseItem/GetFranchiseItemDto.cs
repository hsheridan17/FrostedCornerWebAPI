using FrostedCornerWebAPI.Dtos.Item;

namespace FrostedCornerWebAPI.Dtos.FranchiseItem
{
    public class GetFranchiseItemDto
    {
        public int ItemId { get; set; }
        public GetItemDto Item { get; set; }
        public string CustomColor { get; set; } // Customizations for the item, e.g., "No nuts, extra frosting"
        public float CustomPrice { get; set; } // Price of the franchise item
    }
}
