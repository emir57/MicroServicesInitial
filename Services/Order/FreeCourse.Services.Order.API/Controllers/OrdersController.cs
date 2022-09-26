using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Features.Commands.CreateOrder;
using FreeCourse.Services.Order.Application.Features.Queries.GetOrdersByUserId;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            GetOrdersByUserIdQuery request = new() { UserId = _sharedIdentityService.GetUserId };
            return CreateActionResultInstance(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
        {
            return CreateActionResultInstance(await _mediator.Send(createOrderCommand));
        }
    }
}
