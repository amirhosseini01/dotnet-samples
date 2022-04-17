using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL;
public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UseProjection]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }
}