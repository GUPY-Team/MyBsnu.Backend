using FluentValidation;
using Shared.Core.Features.Queries.Validators;

namespace Modules.Timetable.Core.Features.Schedules.Queries.Validators
{
    public class GetScheduleListQueryValidator : AbstractValidator<GetScheduleListQuery>
    {
        public GetScheduleListQueryValidator()
        {
            Include(new PagedQueryValidator());
        }
    }
}