using Microsoft.EntityFrameworkCore;
using SieveApiSample.Models;

namespace SieveApiSample.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
}