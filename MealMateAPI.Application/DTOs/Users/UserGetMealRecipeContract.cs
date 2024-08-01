namespace MenuMasterAPI.Application.DTOs;

public class UserGetMealRecipeContract
{
    public int Id { get; set; }
    public List<string> MealTypes { get; set; }
}
