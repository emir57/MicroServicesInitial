namespace FreeCourse.Services.Order.Application.Dtos;

public sealed class CreatedOrderDto
{
    public int OrderId { get; set; }
    public CreatedOrderDto() { }
    public CreatedOrderDto(int orderId) : this()
    {
        OrderId = orderId;
    }
}
