using Microsoft.EntityFrameworkCore;
using NpgsqlAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BloggingContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("BloggingContext")!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/blogs", async (BloggingContext context) =>
{
    return  await context.Blogs.ToListAsync();
})
.WithName("GetBlogs")
.WithOpenApi();

app.Run();
