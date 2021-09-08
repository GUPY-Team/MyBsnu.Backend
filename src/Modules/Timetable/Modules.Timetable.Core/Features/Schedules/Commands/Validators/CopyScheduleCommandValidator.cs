using FluentValidation;

namespace Modules.Timetable.Core.Features.Schedules.Commands.Validators
{
    public class CopyScheduleCommandValidator : AbstractValidator<CopyScheduleCommand>
    {
        public CopyScheduleCommandValidator()
        {
            RuleFor(c => c.SourceId).GreaterThan(0);
        }
    }
}