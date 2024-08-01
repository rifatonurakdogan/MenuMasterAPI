using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Application.Services;
using MenuMasterAPI.Domain.Entities;
namespace MenuMasterAPI.Application.Interfaces;
public interface IFoodRecipeService : IBaseService<FoodRecipe, FoodRecipeContract>
{
    Task<List<FoodRecipeGetNameContract>> GetFoodRecipeNamesWithMealTypeAsync(string mealType);
    Task<FoodRecipeContract> GetFoodRecipeWithMealTypeAsync(string mealType);
    Task UpdateFoodRecipeAsync(FoodRecipeContract foodRecipe);
    Task<FoodRecipe> GetFoodRecipeByName(string name);
    Task<List<FoodRecipe>> GetUsersFoodRecipes(int userId);
}