using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.DTOs;

public class UserUpdateContract : IBaseContract
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword {  get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Gender { get; set; }
    public string ActivityStatus { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string>? CuisineNames { get; set; }
}
