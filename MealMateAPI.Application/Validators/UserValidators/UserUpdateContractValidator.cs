using FluentValidation;
using MenuMasterAPI.Application.DTOs;

namespace MenuMasterAPI.Application.Validators;

public class UserUpdateContractValidator : AbstractValidator<UserUpdateContract>
{
    private readonly EnumValidators enumValidators = new EnumValidators();
    public UserUpdateContractValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required.")
            .NotNull().WithMessage("Email cannot be null.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.FullName).Cascade(CascadeMode.Stop)
            .NotEmpty().MinimumLength(5).WithMessage("Please enter at least 5 characters.")
            .NotNull().WithMessage("First name cannot be null.");

        RuleFor(x => x.NewPassword).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Password is required.")
            .NotNull().WithMessage("Password cannot be null.")
            .Length(6).WithMessage("You need to enter exactly 6 characters.");

        RuleFor(x => x.Age).Cascade(CascadeMode.Stop).NotEmpty().NotNull().InclusiveBetween(0, 99).WithMessage("Age must be between 0-99.(Inclusive)");
        RuleFor(x => x.Height).Cascade(CascadeMode.Stop).NotEmpty().NotNull().InclusiveBetween(100, 300).WithMessage("Height must be between 100-300 cm.(Inclusive)");
        RuleFor(x => x.Weight).Cascade(CascadeMode.Stop).NotEmpty().NotNull().InclusiveBetween(30, 200).WithMessage("Weight must be between 30-200 kg.(Inclusive)");
        RuleFor(x => x.Gender).Cascade(CascadeMode.Stop).NotEmpty().NotNull().Must(enumValidators.BeAValidGender).WithMessage("{PropertyName} must be a valid gender.");
        RuleFor(x => x.ActivityStatus).Cascade(CascadeMode.Stop).NotEmpty().NotNull().Must(enumValidators.BeAValidActivity).WithMessage("{PropertyName} must be a valid activity.");
        RuleFor(x => x.DietTypes).Cascade(CascadeMode.Stop).NotEmpty().NotNull().Must(enumValidators.BeAValidDietType).WithMessage("{PropertyName} must be a valid diet type.");
    }
}