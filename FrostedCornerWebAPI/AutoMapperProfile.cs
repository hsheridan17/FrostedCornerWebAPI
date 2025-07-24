using AutoMapper;
using FrostedCornerWebAPI.Dtos.Order;
using FrostedCornerWebAPI.Dtos.OrderItem;
using FrostedCornerWebAPI.Dtos.Item;
using FrostedCornerWebAPI.Dtos.FranchiseItem;
using System.Runtime;
using FrostedCornerWebAPI.Dtos.Supplies;

namespace FrostedCornerWebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Order
            CreateMap<Order, GetOrderDto>();
            CreateMap<AddOrderDto, Order>();

            // Item
            CreateMap<Item, GetItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId));
            CreateMap<AddItemDto, Item>();

            // OrderItem
            CreateMap<OrderItem, GetOrderItemDto>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.FranchiseItem.Item.Name))
                .ForMember(dest => dest.FranchiseItemPrice, opt => opt.MapFrom(src => src.FranchiseItem.CustomPrice));
            CreateMap<AddOrderItemDto, OrderItem>();

            // FranchiseItem
            CreateMap<EditFranchiseItemDto, FranchiseItem>();
            CreateMap<FranchiseItem, GetFranchiseItemDto>();

            // Franchise
            CreateMap<Franchise, GetFranchiseDto>();
            CreateMap<AddFranchiseDto, Franchise>();

            // SuppliesItem
            CreateMap<SuppliesItem, GetSuppliesItemDto>();
            CreateMap<AddSuppliesItemDto, SuppliesItem>();

            // SuppliesOrder
            CreateMap<SuppliesOrder, GetSuppliesOrderDto>()
                .ForMember(dest => dest.SuppliesItems, opt => opt.MapFrom(src => src.SuppliesItems));
            CreateMap<AddSuppliesOrderDto, SuppliesOrder>();

            // DietaryRestriction
            CreateMap<DietaryRestriction, GetDietaryRestrictionDto>();
            CreateMap<AddDietaryRestrictionDto, DietaryRestriction>();
        }
    }
}
