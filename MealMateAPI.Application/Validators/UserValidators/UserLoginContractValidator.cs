using FluentValidation;
using MenuMasterAPI.Application.DTOs;

namespace MenuMasterAPI.Application.Validators.UserValidators;

public class UserLoginContractValidator : AbstractValidator<UserLoginContract>
{
    public UserLoginContractValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Email cannot be null.")
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password required.")
            .NotNull().WithMessage("Password cannot be null.")
            .Length(6).WithMessage("Password should be exactly 6 characters.");
    }
}