using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptor
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntry(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntry(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntry(DbContext? context)
        {
            if (context == null) return;

            foreach(var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Ken";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if(entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasOwnChangeEnteries())
                {
                    entry.Entity.LastModifiedBy = "Sam";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
            throw new NotImplementedException();
        }

    }

    public static class Extension
    {
        public static bool HasOwnChangeEnteries(this EntityEntry entry) =>
            entry.References.Any(r => r.TargetEntry == null && r.TargetEntry.Metadata.IsOwned() &&
                               (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));

    }
}
