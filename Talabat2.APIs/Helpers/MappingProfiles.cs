using AutoMapper;
using Talabat2.APIs.Dtos;
using Talabat2.APIs.Helpers;
using Talabat2.Core.Entites;
//using Talabat2.Core.Entites.Identity;
using Talabat2.Core.Entites.Order_Aggregation;

namespace Talabat2.APIs.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(PD=>PD.ProductBrand,O=>O.MapFrom(P=>P.ProductBrand.Name))
                .ForMember(PD=>PD.ProductType,O=>O.MapFrom(P=>P.ProductType.Name))
                .ForMember(PD=>PD.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Core.Entites.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Address>();
        }
    }
}
