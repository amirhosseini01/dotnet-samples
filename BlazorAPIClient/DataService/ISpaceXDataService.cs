using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public interface ISpaceXDataService
{
    Task<LaunchDto[]> GetLaunches();
}