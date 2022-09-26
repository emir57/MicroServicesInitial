using AutoMapper;

namespace FreeCourse.Services.Order.Application.Mapping;

public static class ObjectMapper
{
    public static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.AddProfile(new CustomMapping());
        });

        IMapper mapper = config.CreateMapper();
        return mapper;
    });
}
