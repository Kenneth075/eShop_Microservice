using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObject
{
    public record OrderId
    {
        public Guid Value { get; }
        private OrderId(Guid value) => Value = value;

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
                throw new DomainException("Orderid cannot be empty");

            return new OrderId(value);

        }
    }

    public record ProductId
    {
        public Guid Value { get; }
        private ProductId(Guid value) => Value = value;

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
                throw new DomainException("ProductId cannot be empty");

            return new ProductId(value);

        }
    }

    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
                throw new DomainException("CustomerId cannot be empty");

            return new CustomerId(value);

        }
    }

    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; }

        private OrderName(string value) => Value=value;

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }
    }


}
