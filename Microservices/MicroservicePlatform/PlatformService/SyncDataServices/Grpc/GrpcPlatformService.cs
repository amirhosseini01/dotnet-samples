using AutoMapper;
using Grpc.Core;
using PlatformService.Data;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepo _platformRepo;
    private readonly IMapper _mapper;
    public GrpcPlatformService(IPlatformRepo platformRepo, IMapper mapper)
    {
        _platformRepo = platformRepo;
        _mapper = mapper;
    }

    public override Task<PlatformResponse> GetAllPaltforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformResponse();
        var platforms = _platformRepo.GetAllPlatforms();

        foreach (var item in platforms)
        {
            response.Platform.Add(_mapper.Map<GrpcPlatformModel>(item));
        }

        return Task.FromResult(response);
    }
}