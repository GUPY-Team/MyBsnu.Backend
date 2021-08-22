using AutoMapper;
using Modules.Identity.Core.Entities;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<SignupUserRequest, AppUser>();
        }
    }
}