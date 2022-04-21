using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL.Commands;
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

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
    {
        var entity = new Command()
        {
            HowTo = input.HowTo,
            CommandLine = input.CommandLine,
            PlatformId = input.PlatformId
        };

        await context.Commands.AddAsync(entity);
        await context.SaveChangesAsync();

        return new AddCommandPayload(entity);
    }
}