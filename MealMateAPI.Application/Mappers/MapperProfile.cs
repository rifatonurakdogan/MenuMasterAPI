using AutoMapper;
using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserContract>();
        CreateMap<User, UserContract>().ReverseMap();

        CreateMap<PagedResponse<User>, PagedResponse<UserContract>>();
        CreateMap<PagedResponse<User>, PagedResponse<UserContract>>().ReverseMap();

        CreateMap<FoodRecipe, FoodRecipeContract>();
        CreateMap<FoodRecipe, FoodRecipeContract>().ReverseMap();

        CreateMap<FoodRecipeContract, FoodRecipeUpdateContractDomain>();
        CreateMap<FoodRecipeContract, FoodRecipeUpdateContractDomain>().ReverseMap();

        CreateMap<PagedResponse<FoodRecipe>, PagedResponse<FoodRecipeContract>>();
        CreateMap<PagedResponse<FoodRecipe>, PagedResponse<FoodRecipeContract>>().ReverseMap();
    }
}
