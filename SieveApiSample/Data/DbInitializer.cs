using SieveApiSample.Models;

namespace SieveApiSample.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Posts.Any())
        {
            return;   // DB has been seeded
        }

        var posts = new Post[]
        {
            new Post { Title = "post A", LikeCount = 100, CommentCount = 10 , DateCreated = DateTimeOffset.Now.AddDays(1)},
            new Post { Title = "post B", LikeCount = 200, CommentCount = 20 , DateCreated = DateTimeOffset.Now.AddDays(2)},
            new Post { Title = "post C", LikeCount = 300, CommentCount = 30 , DateCreated = DateTimeOffset.Now.AddDays(3)},
            new Post { Title = "post D", LikeCount = 400, CommentCount = 40 , DateCreated = DateTimeOffset.Now.AddDays(4)},
            new Post { Title = "post E", LikeCount = 500, CommentCount = 50 , DateCreated = DateTimeOffset.Now.AddDays(5)},
            new Post { Title = "post F", LikeCount = 600, CommentCount = 60 , DateCreated = DateTimeOffset.Now.AddDays(6)},
            new Post { Title = "post G", LikeCount = 700, CommentCount = 70 , DateCreated = DateTimeOffset.Now.AddDays(7)},
            new Post { Title = "post I", LikeCount = 800, CommentCount = 80 , DateCreated = DateTimeOffset.Now.AddDays(8)},
            new Post { Title = "post J", LikeCount = 900, CommentCount = 90 , DateCreated = DateTimeOffset.Now.AddDays(9)},
        };

        foreach (var entity in posts)
        {
            context.Add(entity);
        }

        context.SaveChanges();
    }

    public static void CreateDbIfNotExists(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}