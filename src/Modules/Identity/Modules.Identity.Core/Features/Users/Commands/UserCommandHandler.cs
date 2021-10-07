using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Exceptions;
using Shared.Core.Constants;
using Shared.Core.Helpers;
using Shared.Localization;

namespace Modules.Identity.Core.Features.Users.Commands
{
    public class UserCommandHandler
        : IRequestHandler<CreateUserCommand, Unit>,
            IRequestHandler<DeleteUserCommand, Unit>,
            IRequestHandler<UpdateUserClaimsCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Locale> _localizer;

        public UserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IStringLocalizer<Locale> localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var error = result.Errors.First().Description;
                throw new IdentityException(_localizer.GetString("errors.UnableToCreateUser", error));
            }

            await _userManager.AddClaimsAsync(user, new[]
            {
                new Claim("email", user.Email),
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            });
            
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            Guard.RequireEntityNotNull(user);

            await _userManager.DeleteAsync(user);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var claimsToCheck = request.Claims
                .Select(c => $"{Permissions.PermissionsPrefix}.{c.Value}")
                .ToArray();
            if (!Permissions.All.IsSupersetOf(claimsToCheck))
            {
                var invalidClaims = claimsToCheck.ToHashSet();
                invalidClaims.ExceptWith(Permissions.All);

                var claimsString = string.Join(", ", invalidClaims);
                throw new IdentityException(_localizer.GetString("errors.InvalidClaims", claimsString));
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            Guard.RequireEntityNotNull(user);

            var claims = (await _userManager.GetClaimsAsync(user))
                .Where(c => c.Type == Permissions.PermissionClaimType);

            var result = await _userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                var error = result.Errors.First().Description;
                throw new IdentityException(_localizer.GetString("errors.UnableToRemoveClaims", error));
            }

            var claimsToAdd = _mapper.Map<List<Claim>>(request.Claims);

            await _userManager.AddClaimsAsync(user, claimsToAdd);

            return Unit.Value;
        }
    }
}