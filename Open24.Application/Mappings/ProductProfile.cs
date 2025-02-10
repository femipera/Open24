using AutoMapper;
using Open24.Domain.Entities;
using Open24.Shared.DTOs.Requests;
using Open24.Shared.DTOs.Responses;

namespace Open24.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponse>();

        CreateMap<ProductRequest, Product>()
            .ForMember(p => p.Id, x => x.Ignore()); 
    }
}
