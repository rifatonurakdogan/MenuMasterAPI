
using MenuMasterAPI.Domain.DTOs;
using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Application.DTOs;
public class UserContract : IBaseContract
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Role { get; set; }
    public Gender Gender { get; set; }
    public Activity ActivityStatus { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string>? CuisineNames { get; set; }

}
