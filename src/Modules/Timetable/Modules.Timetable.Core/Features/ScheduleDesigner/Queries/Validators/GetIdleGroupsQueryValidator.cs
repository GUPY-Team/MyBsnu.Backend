using FluentValidation;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries.Validators
{
    public class GetIdleGroupsQueryValidator : AbstractValidator<GetIdleGroupsQuery>
    {
        public GetIdleGroupsQueryValidator()
        {
            Include(new IdleEntityQueryValidator());
        }
    }
}