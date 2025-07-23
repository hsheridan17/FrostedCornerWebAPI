namespace FrostedCornerWebAPI.Models
{
    public class SuppliesOrder
    {
        public int SuppliesOrderId { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        public List<SuppliesItem> SuppliesItems { get; set; }
        public float Total { get; set; }
        public DateTime Time { get; set; }
    }
}
