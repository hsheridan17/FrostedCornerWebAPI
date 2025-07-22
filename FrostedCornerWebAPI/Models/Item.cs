using System.Globalization;

namespace FrostedCornerWebAPI.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; } // List of ingredients, etc.

        // Could also be a list of strings
        public List<DietaryRestriction> DietaryRestrictions { get; set; }
    }
}
