using FrostedCornerWebAPI.Dtos.FranchiseItem;
using FrostedCornerWebAPI.Dtos.Item;
namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class GetSuppliesOrderDto
    {
        public int OrderId { get; set; }
        public GetFranchiseDto Franchise { get; set; }
        public List<GetSuppliesItemDto> SuppliesItems { get; set; }
        public float Total { get; set; }
        public DateTime Time { get; set; }
    }
}
