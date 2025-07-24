namespace FrostedCornerWebAPI.Dtos.Item
{
    public class GetItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageId { get; set; }
        public ItemType ItemType { get; set; }
        public List<GetDietaryRestrictionDto> DietaryRestrictions { get; set; }

    }
}
