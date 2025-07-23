using FrostedCornerWebAPI.Dtos.Item;
namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class AddSuppliesOrderDto
    {
        public int FranchiseId { get; set; }
        public List<GetItemDto> Items { get; set; }
        public float Total { get; set; }
        public DateTime Time { get; set; }
    }
}
