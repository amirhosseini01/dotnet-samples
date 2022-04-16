using GraphQL_HotChocolate.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("CommandConStr")
));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
