using System.Net.Http.Json;
using BlazorAPIClient.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorAPIClient.Pages
{
    public partial class FetchData
    {
        [Inject]
        private HttpClient _http {get; set;}
        private LaunchDto[] launches;

        protected override async Task OnInitializedAsync()
        {
            launches = await _http.GetFromJsonAsync<LaunchDto[]>("https://api.spacex.land/rest/launches");
        }
    }
}