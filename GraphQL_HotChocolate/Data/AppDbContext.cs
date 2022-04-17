using GraphQL_HotChocolate.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_HotChocolate.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }
}