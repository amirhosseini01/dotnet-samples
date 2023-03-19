using EfCacheSample.Configurations;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 builder.Services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider().DisableLogging(true).UseCacheKeyPrefix("EF_")

            // Please use the `CacheManager.Core` or `EasyCaching.Redis` for the Redis cache provider.
            );

            string connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection")!;
            builder.Services.AddConfiguredMsSqlDbContext(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();