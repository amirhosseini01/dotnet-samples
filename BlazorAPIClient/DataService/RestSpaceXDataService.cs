using System.Text.Json;
using BlazorAPIClient.Dtos;

namespace BlazorAPIClient.DataService;
public class RestSpaceXDataService : ISpaceXDataService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    public RestSpaceXDataService(IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
    }
    public async Task<IEnumerable<LaunchDto>> GetLaunches()
    {
         var request = new HttpRequestMessage(HttpMethod.Get,
             _configuration.GetSection("apiBaseUrl").Value + "/rest/launches");

             var client = _clientFactory.CreateClient();

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return  await JsonSerializer.DeserializeAsync
                <IEnumerable<LaunchDto>>(responseStream);
        }
        else
        {
            return new List<LaunchDto>();
        }

    }
}