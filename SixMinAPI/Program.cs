using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// sql server configuration
var sqlServerBuilder = new SqlConnectionStringBuilder();
sqlServerBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
sqlServerBuilder.UserID = builder.Configuration["UserId"];
sqlServerBuilder.Password = builder.Configuration["Password"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlServerBuilder.ConnectionString));

// DI services
builder.Services.AddScoped<CommandRepo>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();