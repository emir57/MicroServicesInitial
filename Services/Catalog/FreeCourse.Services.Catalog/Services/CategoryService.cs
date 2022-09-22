using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services;
internal class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        MongoClient client = new(databaseSettings.ConnectionString);
        IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        List<Category> categories = await _categoryCollection.Find(c => true).ToListAsync();
        List<CategoryDto> result = _mapper.Map<List<CategoryDto>>(categories);
        return Response<List<CategoryDto>>.Success(result, 200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
        CategoryDto result = _mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(result, 201);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        Category category = await _categoryCollection.Find(c => c.Id == id).FirstAsync();
        if (category == null)
            return Response<CategoryDto>.Fail("Category Not Found", 404);

        CategoryDto result = _mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(result, 200);
    }
}
