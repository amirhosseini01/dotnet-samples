using System.Net.Http.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.Pages
{
    public partial class FetchData
    {
        private LaunchDto[] launches;

        protected override async Task OnInitializedAsync()
        {
            launches = await Http.GetFromJsonAsync<LaunchDto[]>("https://api.spacex.land/rest/launches");
        }
    }
}