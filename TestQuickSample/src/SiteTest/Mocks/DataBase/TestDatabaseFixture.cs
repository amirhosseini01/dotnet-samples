using Microsoft.EntityFrameworkCore;
using Site.Data;

namespace SiteTest.Mocks.DataBase;

public class TestDatabaseFixture
{
    private const string ConnectionString = "Server=ServerAddress;Database=DbName;Trusted_Connection=True;TrustServerCertificate=True;";

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
        => new(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(ConnectionString)
                // .UseInMemoryDatabase("SiteTest")
                // .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options);
}