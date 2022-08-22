using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.Models;
using SixMinAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// sql server configuration
var sqlServerBuilder = new SqlConnectionStringBuilder();
sqlServerBuilder.ConnectionString = builder.Configuration
.GetConnectionString("SQLDbConnection");
sqlServerBuilder.UserID = builder.Configuration["UserId"];
sqlServerBuilder.Password = builder.Configuration["Password"];
builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(sqlServerBuilder.ConnectionString));

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
                      builder =>
                       {
                           builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                       }));

// DI services
builder.Services.AddScoped<CommandRepo>();

// auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.MapGet("api/v1/commands", async (CommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAsync();
    return Results.Ok(mapper.Map<IList<Command>>(commands));
});

app.Run();