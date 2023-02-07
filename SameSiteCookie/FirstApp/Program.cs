using FirstApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;

    options.OnAppendCookie = cookieContext =>
        MyUserAgentDetectionLib.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);

    options.OnDeleteCookie = cookieContext =>
        MyUserAgentDetectionLib.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthorization();

app.MapRazorPages();

app.Run();