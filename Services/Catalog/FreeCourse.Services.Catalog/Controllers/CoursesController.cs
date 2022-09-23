using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : CustomBaseController
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        Response<List<CourseDto>> response = await _courseService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        Response<CourseDto> response = await _courseService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("getallbyuserid/{id}")]
    public async Task<IActionResult> GetAllByUserId([FromRoute] string userId)
    {
        Response<List<CourseDto>> response = await _courseService.GetAllByUserIdAsync(userId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CourseCreateDto courseCreateDto)
    {
        Response<CourseDto> response = await _courseService.CreateAsync(courseCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CourseUpdateDto courseUpdateDto)
    {
        Response<NoContent> response = await _courseService.UpdateAsync(courseUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        Response<NoContent> response = await _courseService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
