using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint)
    {
        MongoClient client = new(databaseSettings.ConnectionString);
        IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
    {
        List<Course> courses = await _courseCollection.Find(c => true).ToListAsync();

        if (courses.Any())
            foreach (Course course in courses)
                course.Category = await _categoryCollection
                    .Find(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
        else
            courses = new List<Course>();


        List<CourseDto> result = _mapper.Map<List<CourseDto>>(courses);

        return Shared.Dtos.Response<List<CourseDto>>.Success(result, 200);
    }

    public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string id)
    {
        Course course = await _courseCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (course == null)
            return Shared.Dtos.Response<CourseDto>.Fail("Course not found", 404);

        course.Category = await _categoryCollection
            .Find(c => c.Id == course.CategoryId).FirstOrDefaultAsync();

        CourseDto result = _mapper.Map<CourseDto>(course);

        return Shared.Dtos.Response<CourseDto>.Success(result, 200);
    }

    public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
    {
        List<Course> courses = await _courseCollection.Find(c => c.UserId == userId).ToListAsync();

        if (courses.Any())
            foreach (Course course in courses)
                course.Category = await _categoryCollection
                    .Find(c => c.Id == course.CategoryId).FirstOrDefaultAsync();
        else
            courses = new List<Course>();

        List<CourseDto> result = _mapper.Map<List<CourseDto>>(courses);
        return Shared.Dtos.Response<List<CourseDto>>.Success(result, 200);
    }

    public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        Course addedCourse = _mapper.Map<Course>(courseCreateDto);
        addedCourse.CreatedTime = DateTime.Now;

        await _courseCollection.InsertOneAsync(addedCourse);

        CourseDto result = _mapper.Map<CourseDto>(addedCourse);
        return Shared.Dtos.Response<CourseDto>.Success(result, 201);
    }

    public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
    {
        Course course = _mapper.Map<Course>(courseUpdateDto);

        Course updatedCourse = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == course.Id, course);
        if (updatedCourse == null)
            return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);

        await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent(course.Id, course.Name));

        return Shared.Dtos.Response<NoContent>.Success(204);
    }

    public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string id)
    {
        DeleteResult deleteResult = await _courseCollection.DeleteOneAsync(c => c.Id == id);
        if (deleteResult.DeletedCount > 0)
            return Shared.Dtos.Response<NoContent>.Success(204);

        return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
    }
}
