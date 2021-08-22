using System.Threading.Tasks;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Abstractions
{
    public interface IUserService
    {
        Task SignupUser(SignupUserRequest request);
        Task<UserSignedInResponse> SigninUser(SigninUserRequest request);
    }
}