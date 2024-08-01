using MenuMasterAPI.Domain.Entities;

namespace MenuMasterAPI.Domain.DTOs;

public class UserUpdateContractDomain : IBaseContract
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword {  get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public Gender Gender { get; set; }
    public Activity ActivityStatus { get; set; }
    public DietType DietType { get; set; }
}
