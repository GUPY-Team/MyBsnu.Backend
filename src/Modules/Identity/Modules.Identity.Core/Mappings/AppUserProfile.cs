using AutoMapper;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Features.Auth.Commands;
using Modules.Identity.Core.Features.Users.Commands;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<SignupUserCommand, AppUser>();
            CreateMap<CreateUserCommand, AppUser>();

            CreateMap<AppUser, AppUserListDto>();
        }
    }
}