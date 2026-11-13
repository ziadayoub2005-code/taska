using FluentValidation;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class WorkingHoursViewModelValidator : AbstractValidator<WorkingHoursViewModel>
    {
        public WorkingHoursViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Days)
                .NotEmpty().WithMessage("At least one working day schedule is required.")
                .Must(days => days == null || days.Select(d => d.DayOfWeek).Distinct().Count() == days.Count)
                .WithMessage("Duplicate days of the week are not allowed.");

            RuleForEach(x => x.Days)
                .SetValidator(new WorkingHourViewModelValidator());
        }
    }
}
