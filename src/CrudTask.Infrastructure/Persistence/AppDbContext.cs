using CrudTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudTask.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }
}
