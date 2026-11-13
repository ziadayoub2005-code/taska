using FluentValidation;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class CompleteServiceRequestViewModelValidator : AbstractValidator<CompleteServiceRequestViewModel>
    {
        public CompleteServiceRequestViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ServiceRequestId)
                .GreaterThan(0).WithMessage("Invalid service request.");

            RuleFor(x => x.FinalPrice)
                .NotNull().WithMessage("Final price is required.")
                .GreaterThanOrEqualTo(0m).WithMessage("Final price must be 0 or greater.")
                .PrecisionScale(18, 2, true)
                .WithMessage("Final price cannot exceed 18 digits in total or 2 decimal places.");
        }
    }
}
