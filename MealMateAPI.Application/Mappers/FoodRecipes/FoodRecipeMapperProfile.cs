using AutoMapper;
using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Mappers;

public class FoodRecipeMapperProfile : Profile
{
    public FoodRecipeMapperProfile()
    {
        CreateMap<FoodRecipeContract, FoodRecipeUpdateContractDomain>();
        CreateMap<FoodRecipeContract, FoodRecipeUpdateContractDomain>().ReverseMap();

        CreateMap<FoodRecipe, FoodRecipeGetNameContract>();
        CreateMap<FoodRecipe,FoodRecipeGetNameContract>().ReverseMap();
    }
}
