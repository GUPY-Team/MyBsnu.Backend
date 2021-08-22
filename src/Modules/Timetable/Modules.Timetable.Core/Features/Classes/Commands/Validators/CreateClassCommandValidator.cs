using FluentValidation;

namespace Modules.Timetable.Core.Features.Classes.Commands.Validators
{
    public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
    {
        public CreateClassCommandValidator()
        {
            RuleFor(c => c.Format).GreaterThanOrEqualTo(0);
            RuleFor(c => c.Type).GreaterThanOrEqualTo(0);
            RuleFor(c => c.DayOfWeek).GreaterThanOrEqualTo(0);
            RuleFor(c => c.WeekType).GreaterThanOrEqualTo(0);
            RuleFor(c => c.StartTime.AsTimeSpan()).LessThan(c => c.EndTime.AsTimeSpan());
            RuleFor(c => c.EndTime.AsTimeSpan()).GreaterThan(c => c.StartTime.AsTimeSpan());
            RuleFor(c => c.CourseId).GreaterThan(0);
            RuleFor(c => c.TeacherId).GreaterThan(0);
            RuleFor(c => c.AudienceId).GreaterThan(0).When(c => c.AudienceId != null);
            RuleFor(c => c.ScheduleId).GreaterThan(0);
        }
    }
}