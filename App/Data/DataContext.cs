using App.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
    => options.UseSqlite($"Data Source=micro_reddit.db");

    public override int SaveChanges()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: Timestamp, State: EntityState.Added or EntityState.Modified });

        foreach (EntityEntry entityEntry in entries)
        {
            ((Timestamp)entityEntry.Entity).UpdatedDate = DateTime.Now;
            if (entityEntry.State != EntityState.Added) continue;
            ((Timestamp)entityEntry.Entity).CreatedDate = DateTime.Now;
        }

        return base.SaveChanges();
    }
}