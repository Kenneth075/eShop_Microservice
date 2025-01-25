
namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public IDomainEvent[] ClearDomainEvent()
        {
            IDomainEvent[] dequenedEvent = _domainEvents.ToArray();

            _domainEvents.Clear();
            return dequenedEvent;
        }

        public void AddDomain(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
