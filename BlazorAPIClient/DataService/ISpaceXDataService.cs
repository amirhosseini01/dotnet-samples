using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public interface ISpaceXDataService
{
    Task<IList<LaunchDto>> GetLaunches();
}