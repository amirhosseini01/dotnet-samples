using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Site.Data;

namespace SiteTest.TestingTools.Mocks.DataBase;

public class TestDatabaseFixture
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    // context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    // seed data
                }

                _databaseInitialized = true;
            }
        }
    }

    public ApplicationDbContext CreateContext()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .Build();
        var connectionString = config.GetSection("ConnectionStrings:DefaultConnection").Value;
        return new(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                // .UseInMemoryDatabase("SiteTest")
                // .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options);
    }
}