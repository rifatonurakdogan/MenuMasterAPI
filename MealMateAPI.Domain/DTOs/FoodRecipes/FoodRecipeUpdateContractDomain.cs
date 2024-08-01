using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Domain.DTOs;

public class FoodRecipeUpdateContractDomain : IBaseContract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Ingredients { get; set; }
    public string Recipe { get; set; }
    public MealType MealType { get; set; }
    public bool IsLiked { get; set; }
}
