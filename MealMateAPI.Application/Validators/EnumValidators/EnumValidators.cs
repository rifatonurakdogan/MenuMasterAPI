using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.Validators;

public class EnumValidators
{
    public bool BeAValidGender(string gender)
    {
        return Enum.TryParse(typeof(Gender), gender, true, out _);
    }
    public bool BeAValidActivity(string activity)
    {
        return Enum.TryParse(typeof(Activity), activity, true, out _);
    }
    public bool BeAValidDietType(List<string> dietTypes)
    {
        foreach(string dietType in dietTypes)
        {
            Enum.TryParse(typeof(DietType), dietType, true, out _);
        }
        return true;
    }
    public static bool BeAValidMealType(string mealType)
    {
        return Enum.TryParse(typeof(MealType), mealType, true, out _);
    }
}
