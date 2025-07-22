using FrostedCornerWebAPI.Dtos.FranchiseItem;

namespace FrostedCornerWebAPI.Services.FranchiseItemService
{
    public interface IFranchiseItemService
    {
        public Task<ServiceResponse<List<GetFranchiseDto>>> GetAllFranchises();
        public Task<ServiceResponse<List<GetFranchiseDto>>> AddFranchise(AddFranchiseDto franchise);
        public Task<ServiceResponse<GetFranchiseDto>> AddFranchiseItem(int franchiseId, int itemId);

        public Task<ServiceResponse<GetFranchiseItemDto>> EditFranchiseItem(EditFranchiseItemDto updatedFranchiseItem);


    }
}
