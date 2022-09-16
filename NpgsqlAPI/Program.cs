using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlAPI.API;
using NpgsqlAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("BloggingContext")!;

builder.Services.AddDbContext<BloggingContext>(options =>
        options.UseNpgsql());

builder.Services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<BlogServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/blogs", async (BlogServices service) => await service.GetBlogs())
.WithName("GetBlogs")
.WithOpenApi();

app.Run();
