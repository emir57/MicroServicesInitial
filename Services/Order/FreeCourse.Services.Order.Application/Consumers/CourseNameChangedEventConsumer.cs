using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Application.Consumers;

public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
{
    private readonly OrderDbContext _context;

    public CourseNameChangedEventConsumer(OrderDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
    {
        List<Domain.OrderAggregate.OrderItem> orderItems = await _context.OrderItems.Where(x => x.ProductId == context.Message.CourseId).ToListAsync();
        orderItems.ForEach(orderItem =>
        {
            orderItem.UpdateOrderItem(context.Message.UpatedName, orderItem.PictureUrl, orderItem.Price);
        });
        await _context.SaveChangesAsync();
    }
}
