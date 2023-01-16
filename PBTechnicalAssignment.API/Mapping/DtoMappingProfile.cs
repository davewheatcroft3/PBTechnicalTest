using AutoMapper;
using PBTechnicalAssignment.API.Dtos;
using PBTechnicalAssignment.Core.Model;

namespace PBTechnicalAssignment.API.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.MinimumBinWidth, y => y.Ignore());

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductType, y => y.MapFrom(z => z.ProductType.ToString().ToLower()));
            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(x => x.ProductType, y => y.MapFrom(z => Enum.Parse<Data.Model.ProductType>(z.ProductType.ToUpper())));
        }
    }
}
