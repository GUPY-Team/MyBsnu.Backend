using System;
using System.Collections.Generic;

namespace Shared.Core.Domain
{
    public abstract class DomainException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public DomainException(string message, Dictionary<string, string[]> errors = null)
            : base(message)
        {
            Errors = errors;
        }
    }

    public class EnumerationParseException : DomainException
    {
        public EnumerationParseException(string message, Dictionary<string, string[]> errors = null) : base(message, errors)
        {
        }
    }

    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string message, Dictionary<string, string[]> errors = null)
            : base(message, errors)
        {
        }
    }
}