using FluentValidation;

namespace Modules.Timetable.Core.Features.Schedules.Commands.Validators
{
    public class PublishScheduleCommandValidation : AbstractValidator<PublishScheduleCommand>
    {
        public PublishScheduleCommandValidation()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}