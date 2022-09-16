using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlAPI.API;
using NpgsqlAPI.Data;
using NpgsqlAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("BloggingContext")!;

builder.Services.AddDbContext<BloggingContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddTransient<IDbConnection>(_ => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<IBlogServices, BlogDapperServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/blogs", async (IBlogServices service) => await service.GetList())
.WithName("GetBlogs")
.WithOpenApi();

app.MapGet("/blogs/{id}", async (IBlogServices service, uint id) => await service.Get(id))
.WithName("GetBlog")
.WithOpenApi();

app.MapPost("/blogs", async (IBlogServices service, Blog blog) => await service.Add(blog))
.WithName("AddBlog")
.WithOpenApi();

app.MapDelete("/blogs/{id}", async (IBlogServices service, uint id) => await service.Remove(id))
.WithName("RemoveBlog")
.WithOpenApi();

app.MapPut("/blogs", async (IBlogServices service, Blog blog) => await service.Update(blog))
.WithName("UpdateBlog")
.WithOpenApi();

app.MapPut("/blogs/Bulk", async (IBlogServices service, Blog[] blogs) =>
 await service.UpdateRange(blogs))
.WithName("UpdateBulkBlog")
.WithOpenApi();

app.MapPost("/blogs/Bulk", async (IBlogServices service, Blog[] blogs) =>
 await service.AddRange(blogs))
.WithName("AddBulkBlog")
.WithOpenApi();

app.Run();
