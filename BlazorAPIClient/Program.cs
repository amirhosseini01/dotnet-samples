using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorAPIClient;
using BlazorAPIClient.DataService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();

// builder.Services.AddHttpClient<ISpaceXDataService, RestSpaceXDataService>();
builder.Services.AddHttpClient<ISpaceXDataService, GraphQLSpaceXDataService>();

await builder.Build().RunAsync();
