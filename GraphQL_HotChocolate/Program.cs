using GraphQL_HotChocolate.Data;
using GraphQL_HotChocolate.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("CommandConStr")
));

builder.Services.AddGraphQLServer()
.AddQueryType<Query>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseEndpoints(endpoints =>
    endpoints.MapGraphQL()
);

app.Run();
