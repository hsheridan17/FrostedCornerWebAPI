namespace FrostedCornerWebAPI.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public string FranchiseName { get; set; }
        public string Address { get; set; }
        public List<FranchiseItem> FranchiseItems { get; set; }

        public Franchise()
        {
            this.FranchiseId = 0;
            this.FranchiseName = string.Empty;
            this.Address = string.Empty;
            this.FranchiseItems = new List<FranchiseItem>();

        }
    }
}
