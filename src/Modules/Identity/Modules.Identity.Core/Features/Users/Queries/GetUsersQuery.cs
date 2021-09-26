using System.Collections.Generic;
using MediatR;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<List<AppUserListDto>>
    {
    }
}