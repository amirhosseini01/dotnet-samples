using Site.Helpers.Identity;
using Site.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterContexts();

builder.Services.RegisterIdentityUser();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

builder.Services.RegisterIdentityOptions();
builder.Services.RegisterIdentityApplicationCookie();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
