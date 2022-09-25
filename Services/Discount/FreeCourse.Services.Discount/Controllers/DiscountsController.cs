using FreeCourse.Services.Discount.Models;
using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Service;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountsController : CustomBaseController
{
    private readonly IDiscountService _discountService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
    {
        _discountService = discountService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _discountService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _discountService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]/{code}")]
    public async Task<IActionResult> GetByCode([FromQuery] string code)
    {
        var response = await _discountService.GetByCodeAndUserId(code, _sharedIdentityService.GetUserId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Models.Discount discount)
    {
        var response = await _discountService.SaveAsync(discount);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Models.Discount discount)
    {
        var response = await _discountService.UpdateAsync(discount);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _discountService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }


}
