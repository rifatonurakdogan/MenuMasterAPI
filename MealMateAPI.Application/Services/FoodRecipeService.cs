using AutoMapper;
using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Application.Interfaces;
using MenuMasterAPI.Application.Validators;
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;
using MenuMasterAPI.Domain.Repositories;
using System.Net;

namespace MenuMasterAPI.Application.Services;

public class FoodRecipeService : BaseService<FoodRecipe, FoodRecipeContract>, IFoodRecipeService
{
    private readonly IFoodRecipeRepository _foodRecipeRepository;
    private readonly IMapper _mapper;
    public FoodRecipeService(IGenericRepository<FoodRecipe, FoodRecipeContract> repository, IFoodRecipeRepository foodRecipeRepository, IMapper mapper)
        : base(repository, mapper)
    {
        _foodRecipeRepository = foodRecipeRepository;
        _mapper = mapper;
    }
    public async Task<FoodRecipeContract> GetFoodRecipeWithMealTypeAsync(string mealType)
    {
        var mealTypeEnum = ParseMealType(mealType);
        var model = await _foodRecipeRepository.GetByMealTypeAsync(mealTypeEnum);
        return _mapper.Map<FoodRecipeContract>(model);

    }

    public async Task UpdateFoodRecipeAsync(FoodRecipeContract foodRecipe)
    {
        var mappedContract = _mapper.Map<FoodRecipeUpdateContractDomain>(foodRecipe);
        await _foodRecipeRepository.UpdateAsync(mappedContract);
    }

    public async Task<List<FoodRecipeGetNameContract>> GetFoodRecipeNamesWithMealTypeAsync(string mealType)
    {
        var mealTypeEnum = ParseMealType(mealType);
        var model = await _foodRecipeRepository.GetNamesByMealTypeAsync(mealTypeEnum);
        return _mapper.Map<List<FoodRecipeGetNameContract>>(model);
    }
    private static MealType ParseMealType(string mealType)
    {
        EnumValidators _enumValidators = new EnumValidators();
        var isValidMealType = EnumValidators.BeAValidMealType(mealType);
        if (isValidMealType)
        {
            MealType mealTypeEnum = (MealType)Enum.Parse(typeof(MealType), mealType);
            return mealTypeEnum;
        }
        else
        {
            throw new BaseException(string.Format("Invalid formatted meal type {0}.Please use one of the meal types available. Breakfast, Lunch, Dinner or Snack.", mealType), HttpStatusCode.BadRequest);

        }
    }

    public async Task<FoodRecipe> GetFoodRecipeByName(string name)
    {
        return await _foodRecipeRepository.GetFoodRecipeByName(name);
    }

    public async Task<List<FoodRecipe>> GetUsersFoodRecipes(int userId)
    {
        return await _foodRecipeRepository.GetUsersFoodRecipes(userId);
    }
}
    

