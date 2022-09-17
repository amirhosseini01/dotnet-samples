using System.Text;
using Microsoft.EntityFrameworkCore;
using NpgsqlAPI.Data;
using NpgsqlAPI.Models;

namespace NpgsqlAPI.Services.Blogs;
public sealed class BlogEFRawQueryServices : IBlogServices
{
    private readonly BloggingContext _context;
    public BlogEFRawQueryServices(BloggingContext context)
    {
        _context = context;
    }

    public async Task<uint> Add(Blog obj)
    {
        StringBuilder sb = new();
        sb.AppendLine($"INSERT INTO \"Blogs\"(\"{nameof(Blog.Url)}\")");
        sb.AppendLine("VALUES ({0})");

        return (uint)await _context.Database
        .ExecuteSqlRawAsync(sb.ToString(), obj.Url!);
    }

    public async Task AddRange(Blog[] obj)
    {
        StringBuilder sb = new();
        sb.AppendLine("INSERT INTO \"Blogs\"");
        sb.AppendLine($"(\"{nameof(Blog.Url)}\")");
        sb.AppendLine("VALUES");
        int i;
        for (i = 0; i < obj.Length - 1; i++)
        {
            sb.AppendFormat("({{{0}}}),", i);
        }
        sb.AppendFormat("({{{0}}})", i++);

        await _context.Database
        .ExecuteSqlRawAsync(sb.ToString(), obj.Select(x => x.Url!));
    }

    public async Task<Blog> Get(uint id)
    {
        return null;
        // StringBuilder sb = new();
        // sb.AppendLine("SELECT * FROM \"Blogs\" ");
        // sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        // return await _dbConnection
        // .QueryFirstOrDefaultAsync<Blog>(sb.ToString(), new { blogId = (int)id });
    }

    public async Task<IEnumerable<Blog>> GetList()
    {
        return null;
        // return await _dbConnection.QueryAsync<Blog>("SELECT * FROM \"Blogs\"");
    }

    public async Task Remove(uint id)
    {
        // StringBuilder sb = new();
        // sb.AppendLine("DELETE FROM \"Blogs\" ");
        // sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        // await _dbConnection
        // .ExecuteAsync(sb.ToString(), new { blogId = (int)id });
    }

    public async Task Update(Blog obj)
    {
        // StringBuilder sb = new();
        // sb.AppendLine("UPDATE \"Blogs\"");
        // sb.AppendLine($"SET \"{nameof(Blog.Url)}\" = @blogUrl");
        // sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        // await _dbConnection
        // .ExecuteAsync(sb.ToString(), new { blogId = obj.BlogId, blogUrl = obj.Url });
    }

    public async Task UpdateRange(Blog[] obj)
    {
        // StringBuilder sb = new();
        // sb.AppendLine("UPDATE \"Blogs\"");
        // sb.AppendLine($"SET \"{nameof(Blog.Url)}\" = @url");
        // sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        // await _dbConnection
        // .ExecuteAsync(sb.ToString(), obj);
    }
}