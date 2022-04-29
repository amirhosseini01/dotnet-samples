using System.Net.Http.Json;
using BlazorAPIClient.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorAPIClient.Pages
{
    public partial class FetchData
    {
        [Inject]
        private HttpClient Http {get; set;}
        [Inject]
        private IConfiguration Configuration {get; set;}
        private LaunchDto[] launches;

        protected override async Task OnInitializedAsync()
        {
            launches = await Http.GetFromJsonAsync<LaunchDto[]>(
                Configuration.GetSection("apiBaseUrl").Value + "rest/launches");
        }
    }
}