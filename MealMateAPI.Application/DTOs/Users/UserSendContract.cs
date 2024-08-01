namespace MenuMasterAPI.Application.DTOs;

public class UserSendContract
{
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Gender { get; set; }
    public string ActivityStatus { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string>? CuisineNames { get; set; }
    public List<string>? MealTypes { get; set; }
}
