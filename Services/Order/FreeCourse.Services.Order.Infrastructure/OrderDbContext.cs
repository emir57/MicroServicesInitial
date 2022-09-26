using FreeCourse.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FreeCourse.Services.Order.Infrastructure;

public class OrderDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
    public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
