using System.Collections.Generic;

namespace Shared.DTO.Identity
{
    public class AppUserDto : AppUserListDto
    {
        public IEnumerable<ClaimDto> Claims { get; init; }
    }
}