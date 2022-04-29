using System.Net.Http.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.Pages
{
    public partial class FetchData
    {
        private readonly HttpClient _http;
        public FetchData(HttpClient http)
        {
            _http = http;
        }
        private LaunchDto[] launches;

        protected override async Task OnInitializedAsync()
        {
            launches = await _http.GetFromJsonAsync<LaunchDto[]>("https://api.spacex.land/rest/launches");
        }
    }
}