using Ordering.Domain.Abstractions;
using Ordering.Domain.Model;

namespace Ordering.Domain.Event
{
    public record OrderCreateEvent(Order Order) : IDomainEvent;
    
}
