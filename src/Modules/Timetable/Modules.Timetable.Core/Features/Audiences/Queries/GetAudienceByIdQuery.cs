using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Audiences.Queries
{
    public class GetAudienceByIdQuery : IRequest<AudienceDto>
    {
        public int Id { get; init; }
    }
}