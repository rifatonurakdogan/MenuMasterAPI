using MenuMasterAPI.Application.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Interfaces;

public interface IUserService : IBaseService<User, UserContract>
{
    Task RegisterUserAsync(UserRegisterContract requestModel);
    Task<string> LoginUserAsync(UserLoginContract requestModel);
    Task<UserGetContract> GetUserByIdAsync(int id);
    Task<List<UserGetContract>> GetUsersAsync();
    Task UpdateUserAsync(UserUpdateContract requestModel);
    Task<List<FoodRecipeContract>> GetMealRecipes(UserGetMealRecipeContract requestModel);
    Task<List<FoodRecipeContract>> GetSavedMealRecipes(int id);
    bool ValidatePassword(User user, string password);
    Task<List<FoodRecipeContract>> GetMealRecipesFromImage(string imagePath, int userId);
}
