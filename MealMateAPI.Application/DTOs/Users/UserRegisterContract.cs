﻿namespace MenuMasterAPI.Application.DTOs;
public class UserRegisterContract
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string? Role { get; set; }
    public string Gender { get; set; }
    public string ActivityStatus { get; set; }
    public List<string> DietTypes { get; set; }
    public List<string>? CuisineNames { get; set; }
}
