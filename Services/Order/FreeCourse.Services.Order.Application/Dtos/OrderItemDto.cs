namespace FreeCourse.Services.Order.Application.Dtos;

public sealed class OrderItemDto
{
    public int Id { get; set; }
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string PictureUrl { get; private set; }
    public decimal Price { get; private set; }
}
