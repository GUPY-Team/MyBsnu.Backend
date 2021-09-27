using System.Collections.Generic;
using MediatR;
using Shared.Core.Features.Queries;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<List<AppUserListDto>>, IPagedQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }
}