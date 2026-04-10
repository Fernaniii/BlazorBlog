using BlazorBlog.Domain.Articles;
using BlazorBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Infrastructure;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }




    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditLogs = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog || entry.State == EntityState.Detached)
                continue;

            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
                continue;

            var oldValues = new Dictionary<string, object?>();
            var newValues = new Dictionary<string, object?>();

            foreach (var prop in entry.Properties)
            {
                var name = prop.Metadata.Name;

                if (prop.Metadata.IsPrimaryKey())
                    continue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        newValues[name] = prop.CurrentValue;
                        break;

                    case EntityState.Deleted:
                        oldValues[name] = prop.OriginalValue;
                        break;

                    case EntityState.Modified:
                        if (!prop.IsModified) continue;

                        oldValues[name] = prop.OriginalValue;
                        newValues[name] = prop.CurrentValue;
                        break;
                }
            }

            var audit = AuditLog.Create(
                tableName: entry.Entity.GetType().Name,
                action: entry.State.ToString(),
                userId: null,
                oldValues: oldValues.Count == 0 ? null : oldValues,
                newValues: newValues.Count == 0 ? null : newValues
            );

            auditLogs.Add(audit);
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditLogs.Any())
        {
            AuditLogs.AddRange(auditLogs);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
