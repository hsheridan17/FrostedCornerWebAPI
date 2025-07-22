namespace FrostedCornerWebAPI.Models
{
    // Need this or just use strings?
    public class DietaryRestriction
    {

        public int DietaryRestrictionId { get; set; }
        public string Name { get; set; } // e.g., "Gluten-Free", "Vegan", etc.
         public DietaryRestriction()
        {
            this.DietaryRestrictionId = 0;
            this.Name = string.Empty;
        }
    }
}
