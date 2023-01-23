using Middleware;
using Middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<MessageWriter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// first custom middleware
app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    System.Console.WriteLine("first middleware");
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

// second custom middleware
app.UseRequestCulture();

// third custom middleware
// this middleware using dependencies
app.UseTickWriter();

app.UseAuthorization();

app.MapControllers();

app.Run();
