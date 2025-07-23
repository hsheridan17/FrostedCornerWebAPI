namespace FrostedCornerWebAPI.Models
{
    // This is a class representing a franchise item
    // So it has an id that matches a menu item
    // And allows for customizations
    public class FranchiseItem
    {
        public int FranchiseItemId { get; set; } 
        public int FranchiseId { get; set; } 
        public Franchise Franchise { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string CustomColor { get; set; } 
        public float CustomPrice { get; set; }
        public FranchiseItem()
        {
            this.FranchiseItemId = 0;
            this.FranchiseId = 0;
            this.ItemId = 0;
            this.CustomColor = string.Empty;
            this.CustomPrice = 0;
        }
    }
}

/**
 * Every item shown in the Franchise view IS A FranchiseItem.
 * 
 * When a new franchise is created, FranchiseItems are generated for each Item in menu
 * 
 * The User Sees...
 * FranchiseItem, not the base Item
 * When User adds an item to the cart:
 * Creates an OrderItem using the FranchiseItem
 * 
 */