using Microsoft.EntityFrameworkCore;
using NpgsqlAPI.Models;

namespace NpgsqlAPI.Data;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();
}