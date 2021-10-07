using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Exceptions;
using Shared.DTO.Identity;
using Shared.Localization;

namespace Modules.Identity.Core.Features.Auth.Commands
{
    public class AuthCommandHandler
        : IRequestHandler<SigninUserCommand, UserSignedInResponse>,
            IRequestHandler<SignupUserCommand, Unit>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Locale> _localizer;

        public AuthCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtTokenService jwtTokenService,
            IMapper mapper,
            IStringLocalizer<Locale> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<UserSignedInResponse> Handle(SigninUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new IdentityException(_localizer.GetString("errors.InvalidCredentials"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new IdentityException(_localizer.GetString("errors.InvalidCredentials"));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var token = _jwtTokenService.CreateToken(userClaims);

            return new UserSignedInResponse
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<Unit> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            await _userManager.AddClaimsAsync(user, new[]
            {
                new Claim("email", user.Email),
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            });

            if (!result.Succeeded)
            {
                throw new IdentityException(_localizer.GetString("errors.UnableToSignup", result.Errors.First().Description));
            }

            return Unit.Value;
        }
    }
}