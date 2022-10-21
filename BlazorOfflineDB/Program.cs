using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorOfflineDB;
using BlazorDB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazorDB(options =>
{
    options.Name = "Test";
    options.Version = 1;
    options.StoreSchemas = new List<StoreSchema>()
    {
        new StoreSchema()
        {
            Name = "Person",
            PrimaryKey = "id",
            PrimaryKeyAuto = true,
            UniqueIndexes = new List<string> { "name" }
        }
    };
});

await builder.Build().RunAsync();
