using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Models;

namespace Site.Data;

public class ApplicationDbContext : IdentityDbContext
{
    // dotnet ef migrations add YourMigrationName --context ApplicationDbContext
    // dotnet ef migrations remove --context ApplicationDbContext
    // dotnet ef database update --context ApplicationDbContext
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; } = null!;
}
