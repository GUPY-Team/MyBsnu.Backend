using MediatR;

namespace Modules.Timetable.Core.Features.Groups.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int Id { get; init; }
    }
}