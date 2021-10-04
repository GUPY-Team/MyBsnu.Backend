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

        public EntityNotValidException(string message, Dictionary<string, string[]> errors = null) : base(message)
        {
            Errors = errors;
        }
    }

    public class EntityNotFoundException : DomainException
    {
        public string EntityName { get; }

        public EntityNotFoundException(string entityName) : base($"{entityName} not found")
        {
            EntityName = entityName;
        }
    }

    public class EntityCascadeDeleteRestricted : DomainException
    {
        public string EntityName { get; }
        public EntityCascadeDeleteRestricted(string entityName) : base($"Can't cascade delete {entityName}")
        {
            EntityName = entityName;
        }
    }
}