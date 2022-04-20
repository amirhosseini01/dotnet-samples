using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL;
public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }
    [UseDbContext(typeof(AppDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
    {
        return context.Commands;
    }
}