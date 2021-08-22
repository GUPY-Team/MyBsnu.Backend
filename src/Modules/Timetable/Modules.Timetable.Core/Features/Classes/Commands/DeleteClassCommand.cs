using MediatR;

namespace Modules.Timetable.Core.Features.Classes.Commands
{
    public class DeleteClassCommand : IRequest
    {
        public int Id { get; init; }
    }
}