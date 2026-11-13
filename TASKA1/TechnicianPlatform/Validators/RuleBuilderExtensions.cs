using FluentValidation;

namespace TechnicianPlatform.Validators
{
    /// <summary>
    /// Shared FluentValidation rule extensions matching the database constraints in TechnicianPlatform.DAL.
    /// </summary>
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string?> ValidPhoneNumber<T>(
            this IRuleBuilder<T, string?> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{fieldName} is required.")
                .Length(10, 20).WithMessage($"{fieldName} must be between 10 and 20 digits.")
                .Matches(@"^[0-9]+$").WithMessage($"{fieldName} must contain digits only.");
        }

        public static IRuleBuilderOptions<T, string?> ValidDisplayName<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Display name is required.")
                .MinimumLength(3).WithMessage("Display name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Display name must not exceed 150 characters.");
        }

        public static IRuleBuilderOptions<T, string?> ValidWorkArea<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Work area is required.")
                .MinimumLength(3).WithMessage("Work area must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Work area must not exceed 200 characters.");
        }

        public static IRuleBuilderOptions<T, string?> ValidBio<T>(
            this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters.");
        }

        public static IRuleBuilderOptions<T, double?> ValidLatitude<T>(
            this IRuleBuilder<T, double?> ruleBuilder)
        {
            return ruleBuilder
                .InclusiveBetween(-90.0, 90.0)
                .WithMessage("Latitude must be between -90 and 90.");
        }

        public static IRuleBuilderOptions<T, double?> ValidLongitude<T>(
            this IRuleBuilder<T, double?> ruleBuilder)
        {
            return ruleBuilder
                .InclusiveBetween(-180.0, 180.0)
                .WithMessage("Longitude must be between -180 and 180.");
        }
    }
}
