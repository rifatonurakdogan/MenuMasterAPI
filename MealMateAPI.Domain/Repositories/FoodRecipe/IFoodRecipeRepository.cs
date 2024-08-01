using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Domain.Repositories;

public interface IFoodRecipeRepository : IGenericRepository<FoodRecipe,FoodRecipeUpdateContractDomain>
{
    Task<FoodRecipe> GetByMealTypeAsync(MealType mealType);
    Task<List<FoodRecipe>> GetNamesByMealTypeAsync(MealType mealTypeEnum);
    Task<FoodRecipe> GetFoodRecipeByName(string name);
    Task<List<FoodRecipe>> GetUsersFoodRecipes(int userId);
}
