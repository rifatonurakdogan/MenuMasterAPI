using FluentValidation;
using MenuMasterAPI.Application.DTOs;
namespace MenuMasterAPI.Application.Validators;

public class UserContractValidator : AbstractValidator<UserContract>
{
    public UserContractValidator()
    {
    }
}
