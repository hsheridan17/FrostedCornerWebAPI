using FrostedCornerWebAPI.Dtos.Item;
namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class AddSuppliesOrderDto
    {
        public int FranchiseId { get; set; }
        public List<AddSuppliesItemDto> SuppliesItems { get; set; }
    }
}
