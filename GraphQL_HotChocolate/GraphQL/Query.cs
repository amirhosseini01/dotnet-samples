using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL;
public class Query
{
    public IQueryable<Platform> GetPlatform([Service] AppDbContext context)
    {
        return context.Platforms;
    }
}