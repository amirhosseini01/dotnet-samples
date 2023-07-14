using Microsoft.EntityFrameworkCore;
using Site.Data;

namespace Site.Helpers;

public static class DependencyInjection
{
    public static void RegisterContexts(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    }
}