using NpgsqlAPI.Models;

namespace NpgsqlAPI.Services.Blogs;
public interface IBlogServices
{
    Task<IEnumerable<Blog>> GetList();
    Task<Blog> Get(uint id);
    Task<uint> Add(Blog obj);
    Task AddRange(Blog[] obj);
    Task Update(Blog obj);
    Task UpdateRange(Blog[] obj);
    Task Remove(uint id);
}