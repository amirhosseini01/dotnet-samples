using System.Data;
using System.Text;
using Dapper;
using NpgsqlAPI.Models;

namespace NpgsqlAPI.Services.Blogs;
public sealed class BlogDapperServices : IBlogServices
{
    private readonly IDbConnection _dbConnection;
    public BlogDapperServices(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<uint> Add(Blog obj)
    {
        StringBuilder sb = new();
        sb.AppendLine($"INSERT INTO \"Blogs\"(\"{nameof(Blog.Url)}\")");
        sb.AppendLine("VALUES (@blogUrl)");
        sb.AppendLine($"RETURNING \"{nameof(Blog.BlogId)}\"");

        return await _dbConnection
        .QueryFirstAsync<uint>(sb.ToString(), new { blogUrl = obj.Url });
    }

    public async Task AddRange(Blog[] obj)
    {
        StringBuilder sb = new();
        sb.AppendLine("INSERT INTO \"Blogs\"");
        sb.AppendLine($"(\"{nameof(Blog.Url)}\")");
        sb.AppendLine("VALUES (@url)");

        await _dbConnection
        .ExecuteAsync(sb.ToString(), obj);
    }

    public async Task<Blog> Get(uint id)
    {
        StringBuilder sb = new();
        sb.AppendLine("SELECT * FROM \"Blogs\" ");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        return await _dbConnection
        .QueryFirstOrDefaultAsync<Blog>(sb.ToString(), new { blogId = (int)id });
    }

    public async Task<IEnumerable<Blog>> GetList()
    {
        return await _dbConnection.QueryAsync<Blog>("SELECT * FROM \"Blogs\"");
    }

    public async Task Remove(uint id)
    {
        StringBuilder sb = new();
        sb.AppendLine("DELETE FROM \"Blogs\" ");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        await _dbConnection
        .ExecuteAsync(sb.ToString(), new { blogId = (int)id });
    }

    public async Task Update(Blog obj)
    {
        StringBuilder sb = new();
        sb.AppendLine("UPDATE \"Blogs\"");
        sb.AppendLine($"SET \"{nameof(Blog.Url)}\" = @blogUrl");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        await _dbConnection
        .ExecuteAsync(sb.ToString(), new { blogId = obj.BlogId, blogUrl = obj.Url });
    }

    public async Task UpdateRange(Blog[] obj)
    {
        StringBuilder sb = new();
        sb.AppendLine("UPDATE \"Blogs\"");
        sb.AppendLine($"SET \"{nameof(Blog.Url)}\" = @url");
        sb.AppendLine($"WHERE \"{nameof(Blog.BlogId)}\" = @blogId");

        await _dbConnection
        .ExecuteAsync(sb.ToString(), obj);
    }
}