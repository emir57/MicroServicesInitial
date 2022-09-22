using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Mapping;

public class MapperHelper : Profile
{
    public MapperHelper()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();

        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}
