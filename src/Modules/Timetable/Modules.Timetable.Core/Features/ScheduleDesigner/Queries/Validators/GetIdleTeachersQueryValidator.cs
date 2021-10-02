using FluentValidation;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries.Validators
{
    public class GetIdleTeachersQueryValidator : AbstractValidator<GetIdleTeachersQuery>
    {
        public GetIdleTeachersQueryValidator()
        {
            Include(new IdleEntityQueryValidator());
        }
    }
}