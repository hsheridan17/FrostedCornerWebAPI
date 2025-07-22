using Microsoft.AspNetCore.Routing.Constraints;

namespace FrostedCornerWebAPI.Dtos.OrderItem
{
    public class GetOrderItemDto
    {
        public int FranchiseItemId { get; set; }
        public string ItemName { get; set; }
        public float FranchiseItemPrice { get; set; }
        public int Quantity { get; set; }
        public float SubTotal { get; set; }

    }
}
