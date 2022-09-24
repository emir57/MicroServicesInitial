using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services;

public sealed class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<Response<bool>> DeleteAsync(string userId)
    {
        bool result = await _redisService.GetDb().KeyDeleteAsync(userId);
        return result ?
            Response<bool>.Success(204) :
            Response<bool>.Fail("Basket could not delete", 500);
    }

    public async Task<Response<BasketDto>> GetBasketAsync(string userId)
    {
        RedisValue existBasket = await _redisService.GetDb().StringGetAsync(userId);
        if (String.IsNullOrEmpty(existBasket))
            return Response<BasketDto>.Fail("Basket not found", 404);

        BasketDto basketDto = JsonSerializer.Deserialize<BasketDto>(existBasket);
        return Response<BasketDto>.Success(basketDto, 200);
    }

    public async Task<Response<bool>> SaveOrUpdateAsync(BasketDto basketDto)
    {
        bool result = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return result ?
            Response<bool>.Success(204) :
            Response<bool>.Fail("Basket could not update or save", 500);
    }
}
