using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Entities;
using Shared.Core.Constants;
using Shared.Core.Extensions;
using Shared.Core.Helpers;
using Shared.DTO;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class UserQueryHandler
        : IRequestHandler<GetUserByIdQuery, AppUserDto>,
            IRequestHandler<GetUsersQuery, PagedList<AppUserListDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AppUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            Guard.RequireEntityNotNull(user);

            var userClaims = (await _userManager.GetClaimsAsync(user))
                .Where(c => c.Type == Permissions.PermissionsClaimType);

            return new AppUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = _mapper.Map<List<ClaimDto>>(userClaims)
            };
        }

        public async Task<PagedList<AppUserListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .AsNoTracking()
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalCount = await _userManager.Users.CountAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<AppUserListDto>>(users);
            return new PagedList<AppUserListDto>(mappedItems, request.Page, request.PageSize, totalCount);
        }
    }
}