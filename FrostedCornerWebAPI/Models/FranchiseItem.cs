namespace FrostedCornerWebAPI.Models
{
    // This is a class representing a franchise item
    // So it has an id that matches a menu item
    // And allows for customizations
    public class FranchiseItem
    {
        public int FranchiseItemId { get; set; } 
        public int FranchiseId { get; set; } // Identifier for the associated franchise
        public int ItemId { get; set; } // Identifier for the associated menu item
        
        public Item Item { get; set; }
        public string CustomColor { get; set; } // Customizations for the item, e.g., "No nuts, extra frosting"
        public float CustomPrice { get; set; } // Price of the franchise item
        public FranchiseItem()
        {
            this.FranchiseItemId = 0;
            this.ItemId = 0;
            this.CustomColor = string.Empty;
            this.CustomPrice = 0.0f;
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