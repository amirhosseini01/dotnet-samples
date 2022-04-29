using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorAPIClient;
using BlazorAPIClient.DataService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ISpaceXDataService, RestSpaceXDataService>();
builder.Services.AddScoped<HttpClient>();

await builder.Build().RunAsync();
