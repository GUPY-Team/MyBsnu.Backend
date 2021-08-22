using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Audiences.Queries
{
    public class GetAudiencesQuery : IRequest<List<AudienceDto>>
    {
    }
}