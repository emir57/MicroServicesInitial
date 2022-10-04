using FluentValidation;
using FreeCourse.Services.Order.Application.Dtos;

namespace FreeCourse.Services.Order.Application.Features.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.BuyerId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Address.District)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Address.Line)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Address.Province)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Address.Street)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Address.ZipCode)
            .NotEmpty()
            .NotNull();


        //RuleForEach(c => c.OrderItems).SetValidator(new OrderItemDtoValidator());

    }
}

//public sealed class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
//{
//    public OrderItemDtoValidator()
//    {
//        RuleFor(o => o.ProductName)
//            .NotEmpty()
//            .NotNull();

//        RuleFor(o => o.Price)
//            .NotEmpty()
//            .NotNull()
//            .GreaterThan(0);
//    }
//}
