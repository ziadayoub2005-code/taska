using FluentValidation;
using TechnicianPlatform.DAL.Enums;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Full name must not exceed 150 characters.")
                .Matches(@"^[a-zA-Z\u0600-\u06FF\s]+$")
                .WithMessage("Full name must contain letters and spaces only.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MaximumLength(256).WithMessage("Email must not exceed 256 characters.");

            RuleFor(x => x.PhoneNumber!)
                .ValidPhoneNumber("Phone number");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm password is required.")
                .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Please select a valid account type.")
                .Must(role => role == UserRole.Customer || role == UserRole.Technician)
                .WithMessage("Registration is only allowed as Customer or Technician.");

            When(x => x.Role == UserRole.Technician, () =>
            {
                RuleFor(x => x.CategoryId)
                    .NotNull().WithMessage("Service category is required.")
                    .GreaterThan(0).WithMessage("Please select a valid service category.");

                RuleFor(x => x.DisplayName)
                    .ValidDisplayName();

                RuleFor(x => x.WorkArea)
                    .ValidWorkArea();

                RuleFor(x => x.ContactPhone)
                    .ValidPhoneNumber("Contact phone");

                RuleFor(x => x.Bio)
                    .ValidBio();

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
            });
        }
    }
}
