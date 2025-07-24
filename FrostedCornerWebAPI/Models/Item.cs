using System.Globalization;

namespace FrostedCornerWebAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public ItemType ItemType { get; set; }
        public List<DietaryRestriction> DietaryRestrictions { get; set; }

        public Item()
        {
            this.ItemId = 0;
            this.Name = string.Empty;
            this.Price = 0;
            this.Description = string.Empty;
            this.ItemType = ItemType.Food;
            this.DietaryRestrictions = new List<DietaryRestriction>();
        }
    }
}
