using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreeCourse.Services.Order.Infrastructure.Configs;

public sealed class OrderConfig : IEntityTypeConfiguration<Domain.OrderAggregate.Order>
{
    public void Configure(EntityTypeBuilder<Domain.OrderAggregate.Order> builder)
    {
        builder
            .ToTable("Orders", OrderDbContext.DEFAULT_SCHEMA);

        builder.OwnsOne(o => o.Address)
            .WithOwner();
    }
}
