using Ordering.Domain.Abstractions;
using Ordering.Domain.Enum;
using Ordering.Domain.Event;
using Ordering.Domain.ValueObject;

namespace Ordering.Domain.Model
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _OrderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _OrderItems.AsReadOnly();

        public CustomerId CustomerId { get; set; } = default!;
        public OrderName OrderName { get; set; } = default!;
        public Address BillingAddress { get; set; } = default!;
        public Address ShippingAddress { get; set; } = default!;
        public Payment Payment { get; set; } = default!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x=>x.Price * x.Quantity);

            set { } 
        }

        public static Order Create(OrderId orderId, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
        {
            var order = new Order()
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                BillingAddress = billingAddress,
                ShippingAddress = shippingAddress,
                Payment = payment,
                Status = OrderStatus.Pending,


            };

            order.AddDomainEvent(new OrderCreateEvent(order));

            return order;
        }

        public void Update(OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdateEvent(this));

        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _OrderItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _OrderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem != null)
            {
                _OrderItems.Remove(orderItem);
            }
        }
    }
}
