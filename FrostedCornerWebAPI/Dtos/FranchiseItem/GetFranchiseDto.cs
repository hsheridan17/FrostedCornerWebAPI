namespace FrostedCornerWebAPI.Dtos.FranchiseItem
{
    public class GetFranchiseDto
    {
        public int FranchiseId { get; set; }
        public string FranchiseName { get; set; }
        public string Address { get; set; }
        public List<GetFranchiseItemDto> FranchiseItems { get; set; }
    }
}
