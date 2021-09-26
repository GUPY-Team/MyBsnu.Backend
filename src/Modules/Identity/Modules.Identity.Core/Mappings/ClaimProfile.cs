using System.Security.Claims;
using AutoMapper;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Mappings
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Claim, ClaimDto>();
        }
    }
}