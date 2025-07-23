using FrostedCornerWebAPI.Dtos.FranchiseItem;

namespace FrostedCornerWebAPI.Services.FranchiseItemService
{
    public interface IFranchiseItemService
    {
        public Task<ServiceResponse<List<GetFranchiseDto>>> GetAllFranchises();
        public Task<ServiceResponse<GetFranchiseDto>> GetFranchiseById(int franchiseId);
        public Task<ServiceResponse<GetFranchiseDto>> AddFranchise(AddFranchiseDto franchise);

        public Task<ServiceResponse<GetFranchiseDto>> AddFranchiseItem(int franchiseId, int itemId);
        public Task<ServiceResponse<List<GetFranchiseDto>>> AddItemToAllFranchises(int itemId);

        public Task<ServiceResponse<GetFranchiseItemDto>> EditFranchiseItem(EditFranchiseItemDto updatedFranchiseItem);


    }
}
