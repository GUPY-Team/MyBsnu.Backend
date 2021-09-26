using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Entities;
using Shared.Core.Helpers;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class UserQueryHandler
        : IRequestHandler<GetUserByIdQuery, AppUserDto>,
            IRequestHandler<GetUsersQuery, List<AppUserListDto>>
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
            var user = await _userManager.FindByIdAsync(request.UserId);
            Guard.RequireEntityNotNull(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            return new AppUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Claims = _mapper.Map<List<ClaimDto>>(userClaims)
            };
        }

        public async Task<List<AppUserListDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            return _mapper.Map<List<AppUserListDto>>(users);
        }
    }
}