using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate;

public sealed class Order : Entity, IAggregateRoot
{
    public DateTime CreatedDate { get; private set; }
    public Address Address { get; private set; }
    public string BuyerId { get; private set; }
    private readonly List<OrderItem> _orderItems;

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order(string buyerId, Address address)
    {
        _orderItems = new();
        CreatedDate = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
    {
        bool existProduct = _orderItems.Any(o => o.ProductId == productId);
        if (existProduct == false)
        {
            OrderItem newOrderItem = new(productId, productName, pictureUrl, price);
            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice()
        => _orderItems.Sum(o => o.Price);
}
