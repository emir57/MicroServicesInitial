using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Response<List<CategoryDto>> response = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        Response<CategoryDto> response = await _categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CategoryDto categoryDto)
    {
        Response<CategoryDto> response = await _categoryService.CreateAsync(categoryDto);
        return CreateActionResultInstance(response);
    }
}
