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
            if (result.Succeeded)
            {
                return Unit.Value;
            }

            var error = result.Errors.First().Description;
            throw new IdentityException(_localizer.GetString("errors.UnableToCreateUser", error));
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
            if (!Permissions.All.IsSupersetOf(request.Claims))
            {
                var invalidClaims = request.Claims.ToHashSet();
                invalidClaims.ExceptWith(Permissions.All);

                throw new IdentityException(_localizer.GetString("errors.InvalidClaims", invalidClaims));
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            Guard.RequireEntityNotNull(user);

            var claims = await _userManager.GetClaimsAsync(user);

            var result = await _userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                var error = result.Errors.First().Description;
                throw new IdentityException(_localizer.GetString("errors.UnableToRemoveClaims", error));
            }

            var claimsToAdd = request.Claims.Select(s =>
                new Claim(Permissions.PermissionsClaimType, s.Replace($"{Permissions.PermissionsPrefix}.", ""))
            );

            await _userManager.AddClaimsAsync(user, claimsToAdd);

            return Unit.Value;
        }
    }
}