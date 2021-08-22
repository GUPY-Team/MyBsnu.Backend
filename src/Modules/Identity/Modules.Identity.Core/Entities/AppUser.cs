using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Shared.Core.Domain;

namespace Modules.Identity.Core.Entities
{
    public class AppUser : IdentityUser, IEntity<string>
    {
        private List<DomainEvent> _domainEvents;

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddEvent(DomainEvent @event)
        {
            _domainEvents ??= new List<DomainEvent>();
            _domainEvents.Add(@event);
        }

        public void RemoveEvent(DomainEvent @event)
        {
            _domainEvents?.Remove(@event);
        }

        public void ClearEvents()
        {
            _domainEvents?.Clear();
        }
    }
}