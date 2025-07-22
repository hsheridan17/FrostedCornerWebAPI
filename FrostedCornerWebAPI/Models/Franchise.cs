namespace FrostedCornerWebAPI.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public string FranchiseName { get; set; }
        public string Address { get; set; }
        public List<FranchiseItem> FranchiseItems { get; set; }
    }
}
