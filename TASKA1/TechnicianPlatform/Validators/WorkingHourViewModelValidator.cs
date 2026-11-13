using FluentValidation;
using TechnicianPlatform.ViewModels;

namespace TechnicianPlatform.Validators
{
    public class WorkingHourViewModelValidator : AbstractValidator<WorkingHourViewModel>
    {
        public WorkingHourViewModelValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.DayOfWeek)
                .IsInEnum().WithMessage("Please select a valid day of the week.");

            When(x => !x.IsDayOff, () =>
            {
                RuleFor(x => x.StartTime)
                    .NotNull().WithMessage("Start time is required when it is not a day off.");

                RuleFor(x => x.EndTime)
                    .NotNull().WithMessage("End time is required when it is not a day off.");

                RuleFor(x => x.EndTime)
                    .Must((model, endTime) => endTime > model.StartTime)
                    .WithMessage("End time must be after start time.")
                    .When(x => x.StartTime.HasValue && x.EndTime.HasValue);
            });
        }
    }
}
