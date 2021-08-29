using Shared.Core.Domain;

namespace Modules.Identity.Core.Exceptions
{
    public class IdentityException : DomainException
    {
        public IdentityException(string message) : base(message)
        {
        }
    }
}