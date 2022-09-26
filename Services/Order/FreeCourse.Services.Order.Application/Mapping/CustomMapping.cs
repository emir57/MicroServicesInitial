using AutoMapper;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;

namespace FreeCourse.Services.Order.Application.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        CreateMap<OrderDto, Domain.OrderAggregate.Order>().ReverseMap();
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<OrderItemDto, OrderItem>().ReverseMap();
    }
}
