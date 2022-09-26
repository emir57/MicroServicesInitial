using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreeCourse.Services.Order.Application.Features.Queries.GetOrdersByUserId;

public sealed class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }

    public sealed class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Domain.OrderAggregate.Order> orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.BuyerId == request.UserId).ToListAsync();

            List<OrderDto> orderDtos = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return Response<List<OrderDto>>.Success(orderDtos, 200);
        }
    }
}
