using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public class RestSpaceXDataService : ISpaceXDataService
{
    public Task<LaunchDto[]> GetLaunches()
    {
        throw new NotImplementedException();
    }
}