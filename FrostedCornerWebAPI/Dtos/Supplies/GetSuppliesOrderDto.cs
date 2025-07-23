using FrostedCornerWebAPI.Dtos.FranchiseItem;
using FrostedCornerWebAPI.Dtos.Item;
namespace FrostedCornerWebAPI.Dtos.Supplies
{
    public class GetSuppliesOrderDto
    {
        public int OrderId { get; set; }
        public GetFranchiseDto Franchise { get; set; }
        public List<GetSuppliesItemDto> SuppliesItemDtos { get; set; }
        public float Total { get; set; }
        public DateTime Time { get; set; }
    }
}
