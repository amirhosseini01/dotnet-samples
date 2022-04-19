using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQL_HotChocolate.GraphQL.Platforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents Any software Licence Key");

        descriptor.Field(p => p.LicenseKey).Ignore();

        descriptor.Field(x => x.Commands)
        .ResolveWith<Resolvers>(x => x.GetCommand(default!, default!))
        .UseDbContext<AppDbContext>()
        .Description("This is the list of command in the platform.");
    }

    public class Resolvers
    {
        public IQueryable<Command> GetCommand([Parent] Platform Platforms, [ScopedService] AppDbContext context)
        {
            return context.Commands.Where(x => x.PlatformId == Platforms.Id);
        }
    }
}