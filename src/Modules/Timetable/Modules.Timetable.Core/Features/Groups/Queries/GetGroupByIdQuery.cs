using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Queries
{
    public class GetGroupByIdQuery : IRequest<GroupDto>
    {
        public int Id { get; init; }
    }
}