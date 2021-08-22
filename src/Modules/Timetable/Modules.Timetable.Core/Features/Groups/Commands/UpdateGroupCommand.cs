using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Commands
{
    public class UpdateGroupCommand : IRequest<GroupDto>
    {
        public int Id { get; init; }
        public string Number { get; init; }
    }
}