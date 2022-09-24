using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Service;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Basket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : CustomBaseController
{
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _basketService.GetBasketAsync(_sharedIdentityService.GetUserId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdate([FromBody] BasketDto basketDto)
    {
        var response = await _basketService.SaveOrUpdateAsync(basketDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var response = await _basketService.DeleteAsync(_sharedIdentityService.GetUserId);
        return CreateActionResultInstance(response);
    }
}
