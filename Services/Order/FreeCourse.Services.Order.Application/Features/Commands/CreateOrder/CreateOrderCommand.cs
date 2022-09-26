using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Application.Features.Commands.CreateOrder;

public sealed class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public AddressDto Address { get; set; }

    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Address address = new(request.Address.Province, request.Address.District,
                request.Address.Street, request.Address.ZipCode, request.Address.Line);

            Domain.OrderAggregate.Order order = new(request.BuyerId, address);

            request.OrderItems.ForEach(orderItem =>
            {
                order.AddOrderItem(
                    orderItem.ProductId,
                    orderItem.ProductName,
                    orderItem.Price,
                    orderItem.PictureUrl);
            });


            await _context.Orders.AddAsync(order);
            int row = await _context.SaveChangesAsync();

            return row > 0 ?
                Response<CreatedOrderDto>.Success(new CreatedOrderDto(order.Id), 201) :
                Response<CreatedOrderDto>.Fail("Order add failed", 500);
        }
    }
}
