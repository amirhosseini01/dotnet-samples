using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.Dtos;
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
    return Results.Ok(mapper.Map<IList<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (CommandRepo repo, IMapper mapper, int id) =>
{
    var command = await repo.GetAsync(id);

    if (command is null)
        return Results.NotFound();

    return Results.Ok(mapper.Map<CommandReadDto>(command));
});

app.MapPost("api/v1/commands", async (CommandRepo repo, IMapper mapper, CommandCreateDto dto) =>
{
    var entity = mapper.Map<Command>(dto);

    await repo.AddAsync(entity);
    await repo.SaveChanges();

    var obj = mapper.Map<CommandReadDto>(entity);

    return Results.Created($"api/v1/commands/{obj.Id}", obj);
});

app.MapPut("api/v1/commands/{id}", async
(CommandRepo repo, IMapper mapper, int id, CommandUpdateDto dto) =>
{
    var entity = await repo.GetAsync(id);
    if (entity is null)
        return Results.NotFound();

    mapper.Map(dto, entity);

    await repo.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("api/v1/commands/{id}", async
(CommandRepo repo, int id) =>
{
    var entity = await repo.GetAsync(id);
    if (entity is null)
        return Results.NotFound();

    repo.Remove(entity);
    await repo.SaveChanges();

    return Results.NoContent();
});

app.Run();