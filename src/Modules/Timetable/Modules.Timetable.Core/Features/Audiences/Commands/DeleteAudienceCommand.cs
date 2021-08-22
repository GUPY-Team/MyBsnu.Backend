using MediatR;

namespace Modules.Timetable.Core.Features.Audiences.Commands
{
    public class DeleteAudienceCommand : IRequest
    {
        public int Id { get; init; }
    }
}