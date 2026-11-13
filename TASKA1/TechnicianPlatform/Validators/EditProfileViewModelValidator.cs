using FluentValidation;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class EditProfileViewModelValidator : AbstractValidator<EditProfileViewModel>
    {
        public EditProfileViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.CategoryId)
                .InclusiveBetween(1, int.MaxValue)
                .WithMessage("Please select a valid service category.");

            RuleFor(x => x.DisplayName!)
                .ValidDisplayName();

            RuleFor(x => x.Bio)
                .ValidBio();

            RuleFor(x => x.WorkArea!)
                .ValidWorkArea();

            RuleFor(x => x.ContactPhone!)
                .ValidPhoneNumber("Contact phone");

            RuleFor(x => x.Latitude)
                .ValidLatitude()
                .When(x => x.Latitude.HasValue);

            RuleFor(x => x.Longitude)
                .ValidLongitude()
                .When(x => x.Longitude.HasValue);

            RuleFor(x => x.Latitude)
                .NotNull().WithMessage("Latitude is required when Longitude is provided.")
                .When(x => x.Longitude.HasValue);

            RuleFor(x => x.Longitude)
                .NotNull().WithMessage("Longitude is required when Latitude is provided.")
                .When(x => x.Latitude.HasValue);
        }
    }
}
