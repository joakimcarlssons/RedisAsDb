using RedisAsDb.API.Models;

namespace RedisAsDb.API.Data
{
    public interface IRepository
    {
        void CreatePlatform(Platform platform);
        IEnumerable<Platform?>? GetAllPlatforms();
        Platform? GetPlatformById(string id);
        void UpdatePlatform(Platform platform);
        void DeletePlatform(string id);
    }
}
