using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enum;
using Ordering.Domain.Model;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(x => x.Value, DbId => OrderId.Of(DbId));

            builder.HasOne<Customer>().WithMany().HasForeignKey(x => x.CustomerId).IsRequired();

            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(x => x.OrderName, nameBuilder =>
            {
                nameBuilder.Property(x => x.Value).HasColumnName(nameof(Order.OrderName)).HasMaxLength(100).IsRequired();
            });

            builder.ComplexProperty(x => x.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.EmailAddress).HasMaxLength(100).IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(100).IsRequired();
                addressBuilder.Property(x => x.Country).HasMaxLength(50);
                addressBuilder.Property(x => x.State).HasMaxLength(50);
                addressBuilder.Property(x => x.ZipCode).HasMaxLength(50);
            });

            builder.ComplexProperty(x => x.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
                addressBuilder.Property(x => x.EmailAddress).HasMaxLength(100).IsRequired();
                addressBuilder.Property(x => x.AddressLine).HasMaxLength(100).IsRequired();
                addressBuilder.Property(x => x.Country).HasMaxLength(50);
                addressBuilder.Property(x => x.State).HasMaxLength(50);
                addressBuilder.Property(x => x.ZipCode).HasMaxLength(50);
            });

            builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(x => x.CardNumber).IsRequired();
                paymentBuilder.Property(x => x.CardName).HasMaxLength(50).IsRequired();
                paymentBuilder.Property(x => x.CVV).HasMaxLength(3).IsRequired();
                paymentBuilder.Property(x => x.PaymentMethod);
                paymentBuilder.Property(x => x.Expiration).IsRequired();
            });

            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Draft)
                .HasConversion(x => x.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x => x.TotalPrice);
        }
    }

}
