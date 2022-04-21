using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL.Platforms;
using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL;
public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input, [ScopedService] AppDbContext context)
    {
        var platform = new Platform()
        {
            Name = input.Name
        };

        await context.Platforms.AddAsync(platform);
        await context.SaveChangesAsync();

        return new AddPlatformPayload(platform);
    }
}