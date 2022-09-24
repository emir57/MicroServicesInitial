using FreeCourse.Services.Basket.Settings;
using StackExchange.Redis;

namespace FreeCourse.Services.Basket.Services;

public sealed class RedisService
{
    private readonly RedisSettings _redisSettings;
    private ConnectionMultiplexer _connectionMultiplexer;
    public RedisService(RedisSettings redisSettings)
    {
        _redisSettings = redisSettings;
    }

    public void Connect()
        => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_redisSettings.Host}:{_redisSettings.Port}");

    public IDatabase GetDb(int db = 1)
        => _connectionMultiplexer.GetDatabase(db);
}
