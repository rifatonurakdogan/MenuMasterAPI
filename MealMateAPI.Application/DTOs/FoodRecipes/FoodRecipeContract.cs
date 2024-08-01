using MenuMasterAPI.Domain.DTOs;

namespace MenuMasterAPI.Application.DTOs;

public class FoodRecipeContract : IBaseContract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public string Recipe { get; set; }
    public string MealType {  get; set; }
    public bool IsLiked { get; set; }
}
