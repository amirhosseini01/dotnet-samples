using System.Net.Http.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public class RestSpaceXDataService : ISpaceXDataService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public RestSpaceXDataService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(_configuration.GetSection("apiBaseUrl").Value);
        _configuration = configuration;
    }
    public async Task<IList<LaunchDto>> GetLaunches()
    {
        return await _httpClient.GetFromJsonAsync<List<LaunchDto>>(
            "/rest");
    }
}