using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.DatabaseExtension
{
    public static class InitializeDatabase
    {
        public static async Task DatabaseInitialization(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderAsync(context);
        }

        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }

        
        private static async Task SeedProductAsync(ApplicationDbContext context)
        {
            if(!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedOrderAsync(ApplicationDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.OrderWithItems);
                await context.SaveChangesAsync();
            }
        }



    }
}
