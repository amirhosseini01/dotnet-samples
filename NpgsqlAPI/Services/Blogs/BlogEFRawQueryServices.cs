using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

    public async Task<Blog?> Get(uint id)
    {
        StringBuilder sb = new();
        sb.AppendLine("SELECT * FROM \"Blogs\" ");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = {{0}}");

        return await _context.Blogs
        .FromSqlRaw(sb.ToString(), (int)id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Blog>> GetList()
    {
        return await _context.Blogs.FromSqlRaw("SELECT * FROM \"Blogs\"").ToListAsync();
    }

    public async Task Remove(uint id)
    {
        StringBuilder sb = new();
        sb.AppendLine("DELETE FROM \"Blogs\" ");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = {{0}}");
        await _context.Database
        .ExecuteSqlRawAsync(sb.ToString(), (int)id);
    }

    public async Task Update(Blog obj)
    {
        StringBuilder sb = new();
        sb.AppendLine("UPDATE \"Blogs\"");
        sb.AppendLine($"SET \"{nameof(Blog.Url)}\" = {{1}}");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = {{0}}");

        await _context.Database
        .ExecuteSqlRawAsync(sb.ToString(), obj.BlogId, obj.Url!);
    }

    public async Task UpdateRange(Blog[] obj)
    {
        StringBuilder sb = new();
        List<NpgsqlParameter> sqlParameters = new();

        for (int i = 0; i < obj.Length; i++)
        {
            sqlParameters.Add(new NpgsqlParameter($"url{i}", obj[i].Url));
            sqlParameters.Add(new NpgsqlParameter($"blogId{i}", obj[i].BlogId));
            sb.AppendLine(" UPDATE \"Blogs\"");
            sb.AppendFormat($"SET \"{nameof(Blog.Url)}\" = @url{i}");
            sb.AppendFormat($" WHERE \"{nameof(Blog.BlogId)}\" = @blogId{i};");
        }

        await _context.Database
        .ExecuteSqlRawAsync(sb.ToString(), sqlParameters);
    }
}