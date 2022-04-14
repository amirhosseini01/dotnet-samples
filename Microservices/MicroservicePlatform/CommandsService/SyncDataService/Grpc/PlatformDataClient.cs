using System.Diagnostics;
using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataService.Grpc;

public class PlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        Debug.WriteLine("--> Calling Grpc Service ...");
        var channel = GrpcChannel.ForAddress(_configuration["GrpsPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = client.GetAllPaltforms(request);
            return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"--> Could not call Grpc Server: {ex.Message} {ex.InnerException?.Message}");
            Console.WriteLine($"--> Could not call Grpc Server: {ex.Message} {ex.InnerException?.Message}");
            return null;
        }
    }
}