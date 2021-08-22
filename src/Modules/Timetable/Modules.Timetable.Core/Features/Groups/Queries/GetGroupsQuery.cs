using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Queries
{
    public class GetGroupsQuery : IRequest<List<GroupDto>>
    {
    }
}