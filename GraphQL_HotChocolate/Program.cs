using GraphQL.Server.Ui.Voyager;
using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL;
using GraphQL_HotChocolate.GraphQL.Commands;
using GraphQL_HotChocolate.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("CommandConStr")
));

builder.Services.AddGraphQLServer()
.AddQueryType<Query>()
.AddMutationType<Mutation>()
.AddSubscriptionType<Subscription>()
.AddType<PlatformType>()
.AddType<CommandType>()
.AddFiltering()
.AddSorting()
.AddProjections()
.AddInMemorySubscriptions();

var app = builder.Build();

app.UseWebSockets();

app.MapGraphQL();
app.UseGraphQLVoyager(options: new VoyagerOptions(){
    GraphQLEndPoint = "/graphql"
}, path: "/graphql-voyager");

app.Run();
