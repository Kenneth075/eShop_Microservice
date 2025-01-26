using Ordering.Domain.Abstractions;
using Ordering.Domain.Model;

namespace Ordering.Domain.Event
{
    public record OrderUpdateEvent(Order Order) : IDomainEvent;
    
}
