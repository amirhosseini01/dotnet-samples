using RedisAPI.Models;

namespace RedisAPI.Data;

public interface IPlatformRepo
{
    Task Create(Platform platform);
    Task<Platform> GetById(string id);
    IEnumerable<Platform> GetAll();
}