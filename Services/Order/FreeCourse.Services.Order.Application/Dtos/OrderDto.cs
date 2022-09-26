using FreeCourse.Services.Order.Domain.OrderAggregate;

namespace FreeCourse.Services.Order.Application.Dtos;

public sealed class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; private set; }
    public AddressDto Address { get; private set; }
    public string BuyerId { get; private set; }
    private List<OrderItemDto> OrderItems { get; set; }
}
