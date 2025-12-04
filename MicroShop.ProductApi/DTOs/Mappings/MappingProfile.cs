using AutoMapper;
using MicroShop.ProductApi.Models;

namespace MicroShop.ProductApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();

        CreateMap<ProductDTO, Product>();

        CreateMap<Product, ProductDTO>()
            .ForMember(c => c.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

    }
}
