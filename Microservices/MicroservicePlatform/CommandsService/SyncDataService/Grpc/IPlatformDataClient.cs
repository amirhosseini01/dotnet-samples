using CommandsService.Models;

namespace CommandsService.SyncDataService.Grpc;

public interface IPlatformDataClient
{
    IEnumerable<Platform> ReturnAllPlatforms();
}