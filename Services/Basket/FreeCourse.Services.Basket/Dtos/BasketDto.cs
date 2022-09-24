namespace FreeCourse.Services.Basket.Dtos;

public sealed class BasketDto
{
    public string? UserId { get; set; }
    public string? DiscountCode { get; set; }
    public List<BasketItemDto> BasketItems { get; set; }

    public decimal TotalPrice
        => BasketItems.Sum(y => y.Price);
}
