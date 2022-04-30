using System.Text.Json.Serialization;

namespace BlazorAPIClient.Dtos;

public class GqlParentData
{
    [JsonPropertyName("data")]
    public GqlData Data { get; set; }
}
public class GqlData
{
    [JsonPropertyName("launches")]
    public LaunchDto[] Launches { get; set; }
}
public class LaunchDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("is_tentative")]
    public bool IsTentative { get; set; }

    [JsonPropertyName("launch_date_local")]
    public DateTimeOffset LaunchDateLocal { get; set; }

    [JsonPropertyName("mission_name")]
    public string MissionName { get; set; }
}