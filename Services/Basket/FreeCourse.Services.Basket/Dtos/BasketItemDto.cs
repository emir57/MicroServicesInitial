namespace FreeCourse.Services.Basket.Dtos;

public sealed class BasketItemDto
{
    public byte Quantity { get; set; }
    public string? CourseId { get; set; }
    public string? CourseName { get; set; }
    public decimal Price { get; set; }
}
