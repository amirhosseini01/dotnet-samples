using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.Models;

namespace GraphQL_HotChocolate.GraphQL.Commands;
public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command in Platform");

        descriptor.Field(x=> x.Platform)
        .ResolveWith<Resolvers>(x=> x.GetPlatform(default!, default!))
        .UseDbContext<AppDbContext>()
        .Description("This Platform For Command");
    }

    public class Resolvers
    {
        public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.FirstOrDefault(x => x.Id == command.PlatformId);
        }
    }
}