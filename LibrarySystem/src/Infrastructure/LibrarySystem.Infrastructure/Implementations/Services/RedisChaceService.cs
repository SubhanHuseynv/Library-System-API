using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LibrarySystem.Infrastructure.Implementations.Services;
public class RedisCacheService : IRedisCacheService
{
    private readonly ConnectionMultiplexer redisConnection;
    private readonly StackExchange.Redis.IDatabase database;
    private readonly RedisCacheOptions settings;
    public RedisCacheService(IOptions<RedisCacheOptions> options)
    {
        settings = options.Value;
        var opt = ConfigurationOptions.Parse(settings.Configuration);
        redisConnection = ConnectionMultiplexer.Connect(opt);
        database = redisConnection.GetDatabase();
    }
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await database.StringGetAsync(key);
        if (value.HasValue)
            return JsonConvert.DeserializeObject<T>(value);

        return default;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? expirationTime = null)
    {
        TimeSpan timeUnitExpiration = expirationTime.Value - DateTime.Now;
        await database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUnitExpiration);
    }
}
