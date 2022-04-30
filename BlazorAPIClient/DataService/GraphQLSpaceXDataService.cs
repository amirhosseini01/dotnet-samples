using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public class GraphQLSpaceXDataService : ISpaceXDataService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public GraphQLSpaceXDataService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri(_configuration.GetSection("apiBaseUrl").Value);
    }
    public async Task<LaunchDto[]> GetLaunches()
    {
        var queryObject = new
        {
            query = "{launches {id is_tentative launch_date_local mission_name}}",
            variables = new { }
        };

        var launchQuery = new StringContent(
            JsonSerializer.Serialize(queryObject),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("/graphql", launchQuery);

        if (!response.IsSuccessStatusCode) return null;

        var graphQlResult = await JsonSerializer.DeserializeAsync<GqlParentData>(await response.Content.ReadAsStreamAsync());

        return graphQlResult.Data.Launches;

    }
}