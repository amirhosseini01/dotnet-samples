using BlazorAPIClient.DataService;
using BlazorAPIClient.Dtos;
using Microsoft.AspNetCore.Components;

namespace BlazorAPIClient.Pages;
public partial class Launches
{
    [Inject]
    private ISpaceXDataService SpaceXDataService { get; set; }
    private IList<LaunchDto> launches;

    protected override async Task OnInitializedAsync()
    {
        launches = await SpaceXDataService.GetLaunches();
    }
}