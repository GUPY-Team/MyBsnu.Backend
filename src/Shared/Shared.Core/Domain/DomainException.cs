using System;
using System.Collections.Generic;

namespace Shared.Core.Domain
{
    public abstract class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }

    public class EntityNotValidException : DomainException
    {
        public Dictionary<string, string[]> Errors { get; }

        public EntityNotValidException(string message, Dictionary<string, string[]> errors) : base(message)
        {
            Errors = errors;
        }
    }

    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entityName) : base($"{entityName} not found")
        {
        }
    }
}