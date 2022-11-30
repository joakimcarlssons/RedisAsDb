using RedisAsDb.API.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisAsDb.API.Data
{
    public class RedisRepository : IRepository
    {
        private const string HashKey = "Platforms";
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform == null) throw new ArgumentNullException(nameof(platform));
            _db.HashSet(HashKey, new HashEntry[]
            {
                new HashEntry(platform.Id, JsonSerializer.Serialize(platform))
            }); // --> Stores the platform in a hash set, making it easier to get all platforms etc

            //db.StringSet(platform.Id, JsonSerializer.Serialize(platform)); // --> Sets a single value as a string
        }

        public void DeletePlatform(string id)
        {
            if (_db.HashGet(HashKey, id).IsNull) return;
            _db.HashDelete(HashKey, id);
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            var platforms = _db.HashGetAll(HashKey);

            if (platforms is not null && platforms?.Length > 0)
            {
                return Array.ConvertAll(platforms, item => JsonSerializer.Deserialize<Platform?>(item.Value));
            }

            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            var platform = _db.HashGet(HashKey, id);
            
            //var platform = db.StringGet(id); // --> Get a single item as a string (must be stored as string)

            if (!String.IsNullOrEmpty(platform))
            {
                return JsonSerializer.Deserialize<Platform>(platform);
            }

            return null;
        }

        public void UpdatePlatform(Platform platform)
        {
            if (_db.HashGet(HashKey, platform.Id).IsNull) return;

            _db.HashSet(HashKey, new HashEntry[]
            {
                new HashEntry(platform.Id, JsonSerializer.Serialize(platform))
            });
        }
    }
}
