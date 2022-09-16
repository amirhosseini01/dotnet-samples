using System.Data;
using Dapper;
using NpgsqlAPI.Data;
using NpgsqlAPI.Models;

namespace NpgsqlAPI.API;
public sealed class BlogServices
{
    private readonly IDbConnection _dbConnection;
    public BlogServices(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<IEnumerable<Blog>> GetBlogs()
    {
        return await _dbConnection.QueryAsync<Blog>("SELECT * FROM \"Blogs\"");
    }
}