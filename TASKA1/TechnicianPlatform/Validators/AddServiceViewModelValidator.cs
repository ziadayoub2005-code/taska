using FluentValidation;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class AddServiceViewModelValidator : AbstractValidator<AddServiceViewModel>
    {
        public AddServiceViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required.")
                .MinimumLength(3).WithMessage("Service name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Service name must not exceed 150 characters.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Service price is required.")
                .GreaterThan(0m).WithMessage("Service price must be greater than 0.")
                .PrecisionScale(18, 2, true)
                .WithMessage("Service price cannot exceed 18 digits in total or 2 decimal places.");
        }
    }
}
