using Microsoft.EntityFrameworkCore;
using SixMiniAPI.Models;

namespace SixMiniAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Command> Commands => Set<Command>();
}