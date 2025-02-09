using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Model;
using Ordering.Domain.ValueObject;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasConversion(customerId => customerId.Value, DbId => CustomerId.Of(DbId));

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(100);
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
