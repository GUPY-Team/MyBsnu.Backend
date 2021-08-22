using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Audiences.Commands
{
    public class UpdateAudienceCommand : IRequest<AudienceDto>
    {
        public int Id { get; init; }
        
        public int Corps { get; init; }
        public int Floor { get; init; }
        public int Room { get; init; }
    }
}