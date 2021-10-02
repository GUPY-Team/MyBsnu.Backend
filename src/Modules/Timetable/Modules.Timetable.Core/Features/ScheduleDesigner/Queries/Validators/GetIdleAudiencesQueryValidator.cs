using FluentValidation;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries.Validators
{
    public class GetIdleAudiencesQueryValidator : AbstractValidator<GetIdleAudiencesQuery>
    {
        public GetIdleAudiencesQueryValidator()
        {
            Include(new IdleEntityQueryValidator());
        }
    }
}