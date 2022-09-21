using Microsoft.EntityFrameworkCore;
using NpgsqlAPI.Data;
using NpgsqlAPI.Models;

namespace NpgsqlAPI.Services.Blogs;
public sealed class BlogEFServices : IBlogServices
{
    private readonly BloggingContext _context;
    public BlogEFServices(BloggingContext context)
    {
        _context = context;
    }

    public async Task<uint> Add(Blog obj)
    {
        await _context.Blogs.AddAsync(obj);
        return (uint)await SaveChangesAsync();
    }

    public async Task AddRange(Blog[] obj)
    {
        await _context.Blogs.AddRangeAsync(obj);
        await SaveChangesAsync();
    }

    public async Task<Blog?> Get(uint id)
    {
        return await _context.Blogs.FirstOrDefaultAsync(x=>x.BlogId == id);
    }

    public async Task<IEnumerable<Blog>> GetList()
    {
       return await _context.Blogs.ToListAsync();
    }

    public async Task Remove(uint id)
    {
        var entity = await Get(id);
        _context.Blogs.Remove(entity!);
        await SaveChangesAsync();
    }

    public async Task Update(Blog obj)
    {
        _context.Blogs.Update(obj);
        await SaveChangesAsync();
    }

    public async Task UpdateRange(Blog[] obj)
    {
        _context.Blogs.UpdateRange(obj);
        await SaveChangesAsync();
    }

    private async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}