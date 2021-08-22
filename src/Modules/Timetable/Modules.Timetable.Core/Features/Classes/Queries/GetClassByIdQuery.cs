using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Classes.Queries
{
    public class GetClassByIdQuery : IRequest<ClassDto>
    {
        public int Id { get; init; }
    }
}