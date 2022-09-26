using AutoMapper;

namespace FreeCourse.Services.Order.Application.Mapping;

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(configure =>
        {
            configure.AddProfile(new CustomMapping());
        });

        IMapper mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => _lazy.Value;
}
