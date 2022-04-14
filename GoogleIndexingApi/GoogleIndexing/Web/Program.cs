using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Web.Jobs;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string connectionStr = builder.Configuration.GetConnectionString("DbContextConnectionStr");
builder.Services.AddDbContext<MyDatabaseContext>(options =>
  options.UseSqlServer(connectionStr));

builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionStr, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IJobs, Jobs>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
