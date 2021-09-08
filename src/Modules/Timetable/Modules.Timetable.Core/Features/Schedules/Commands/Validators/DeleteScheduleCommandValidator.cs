using FluentValidation;

namespace Modules.Timetable.Core.Features.Schedules.Commands.Validators
{
    public class DeleteScheduleCommandValidator : AbstractValidator<DeleteScheduleCommand>
    {
        public DeleteScheduleCommandValidator()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}