using FreeCourse.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeCourse.Services.Order.Infrastructure.Configs;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder
            .ToTable("OrderItems", OrderDbContext.DEFAULT_SCHEMA);

        builder.Property(o => o.Price)
            .HasColumnType("decimal(18,2)");

    }
}
