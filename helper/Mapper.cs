namespace WebApi.Helpers;

using AutoMapper;
using ECommerce.Models.Dtos.User;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUser, User>();
        CreateMap<User, UserResponse>();
        CreateMap<CreateAddress, Address>();
        CreateMap<Address, AddressResponse>();
        CreateMap<Category, CategoryDetails>();
        CreateMap<CreateCategory, Category>();
        CreateMap<Product, ProductDetail>().ReverseMap();
        CreateMap<Product, CreateProducts>().ReverseMap();
    }
}
