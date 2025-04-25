using AutoMapper;
using Backend.Models;
using Backend.Dtos;

namespace Backend.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            // Map Receipt to ReceiptDto
            CreateMap<Receipt, ReceiptDto>()
                .ForMember(dest => dest.ReceiptProducts, opt => opt.MapFrom(src => src.ReceiptProducts));
            // Map ReceiptProduct to ReceiptProductDto
            CreateMap<ReceiptProduct, ReceiptProductDto>();
        }
    }
}
