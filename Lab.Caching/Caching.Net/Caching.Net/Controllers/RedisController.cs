using Caching.Net.RedisCacheServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Caching.Net.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDistributedCache _cache;
        private readonly ICacheService _cacheService;
        private static object _lock = new object();

        public RedisController(IDistributedCache cache, ICacheService cacheService)
        {
            _cache = cache;
            _cacheService = cacheService;
        }

        [HttpGet("redis-cache")]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "weathers";

            byte[] cachedData = await _cache.GetAsync(cacheKey);

            if (cachedData != null)
            {
                // If the data is found in the cache, encode and deserialize cached data.
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                var data = JsonSerializer.Deserialize<List<WeatherForecast>>(cachedDataString);
                return Ok(data);
            }
            else
            {
                // sleep 3s
                Thread.Sleep(3000);

                var rng = new Random();
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                // Serializing the data
                string cachedDataString = JsonSerializer.Serialize(data);
                var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // Setting up the cache options
                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddSeconds(50)) // set the expiration time of the cached object.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10)); // similar to Absolute Expiration. It expires as a cached object if it not being requested for a defined amount of time period. Note that Sliding Expiration should always be set lower than the absolute expiration.

                // Add the data into the cache
                await _cache.SetAsync(cacheKey, dataToCache, options);

                return Ok(data);
            }
        }

        [HttpGet("db-redis-cache")]
        public IEnumerable<WeatherForecast> Get()
        {
            var cacheKey = "db_weathers";
            var cacheData = _cacheService.GetData<IEnumerable<WeatherForecast>>(cacheKey);
            if (cacheData != null)
                return cacheData;

            lock (_lock)
            {
                // sleep 3s
                Thread.Sleep(3000);

                var expirationTime = DateTimeOffset.Now.AddSeconds(10.0);

                var rng = new Random();
                cacheData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                _cacheService.SetData<IEnumerable<WeatherForecast>>(cacheKey, cacheData, expirationTime);
            }
            return cacheData;
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            _cacheService.RemoveData(key);
            return Ok();
        }
    }
}
