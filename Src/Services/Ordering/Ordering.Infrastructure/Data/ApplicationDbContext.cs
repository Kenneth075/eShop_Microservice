using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Model;
using Ordering.Domain.ValueObject;
using System.Reflection;

namespace Ordering.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }


    }

    
}
