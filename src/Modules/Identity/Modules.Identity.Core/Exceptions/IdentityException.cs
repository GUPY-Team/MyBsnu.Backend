using System.Collections.Generic;
using Shared.Core.Domain;

namespace Modules.Identity.Core.Exceptions
{
    public class IdentityException : DomainException
    {
        public IdentityException(string message, Dictionary<string, string[]> errors = null) : base(message, errors)
        {
        }
    }
}