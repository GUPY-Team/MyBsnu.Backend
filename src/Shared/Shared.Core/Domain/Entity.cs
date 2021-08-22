using System.Collections.Generic;

namespace Shared.Core.Domain
{
    public interface IEntity
    {
        public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
        public void AddEvent(DomainEvent @event);
        public void RemoveEvent(DomainEvent @event);
        public void ClearEvents();
    }

    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

    public abstract class Entity<TId> : IEntity<TId>
    {
        private List<DomainEvent> _domainEvents;
        
        public TId Id { get; set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

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