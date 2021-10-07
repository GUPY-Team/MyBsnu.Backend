using FluentValidation;
using Shared.Core.Constants;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries.Validators
{
    public class IdleEntityQueryValidator : AbstractValidator<IIdleEntityQuery>
    {
        public IdleEntityQueryValidator()
        {
            RuleFor(q => q.WeekDay).NotEmpty();
            RuleFor(q => q.StartTime).NotEmpty().Matches(CommonConstants.Regex.Time);
            RuleFor(q => q.ScheduleId).GreaterThanOrEqualTo(1);
        }
    }
}