using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Exceptions;
using Shared.DTO.Identity;
using Shared.Localization;

namespace Modules.Identity.Core.Features.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Locale> _localizer;

        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            IStringLocalizer<Locale> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task SignupUser(SignupUserRequest request)
        {
            var user = _mapper.Map<AppUser>(request);

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new IdentityException(_localizer.GetString("errors.UnableToSignup", result.Errors.First().Description));
            }
        }

        public async Task<UserSignedInResponse> SigninUser(SigninUserRequest request)
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
            var token = _tokenService.CreateToken(userClaims);

            return new UserSignedInResponse
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }
    }
}