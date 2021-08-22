using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Commands
{
    public class CreateGroupCommand : IRequest<GroupDto>
    {
        public string Number { get; init; }
    }
}