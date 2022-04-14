using System.Diagnostics;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;

namespace CommandsService.EventProcessing;
public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
        _scopeFactory = scopeFactory;
        _mapper = mapper;
    }
    public void ProcessEvent(string message)
    {
        EventType eventType = DetermineEvent(message);
        switch (eventType)
        {
            case EventType.PaltformPublished:
                AddPlatform(message);
                break;
            default:
                break;
        }
    }

    private void AddPlatform(string platformPublishedMessage)
    {
        using var scope = _scopeFactory.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
            var plat = _mapper.Map<Platform>(platformPublishedDto);
            if (repo.IsExistExternalPlatform(plat.ExternalId))
            {
                Debug.WriteLine($"--> Platform with ExternalId: {plat.ExternalId} Already Exist");
                return;
            }

            repo.Create(plat);
            repo.SaveChanges();
            Debug.WriteLine($"--> {plat.Name} Platform, Added SuccessFully. platID: {plat.Id}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"--> Could Not Add Platform to DB {ex.Message} {ex.InnerException?.Message}");
        }
    }
    private static EventType DetermineEvent(string notificationMessage)
    {
        Debug.WriteLine("--> Determining Event");

        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        switch (eventType.Event)
        {
            case nameof(EventTypeWebService.Platform_Published):
                Debug.WriteLine("--> Platform Published Event Detected");
                return EventType.PaltformPublished;

            default:
                Debug.WriteLine("--> Could Not Determine Event Type");
                return EventType.Undetermined;
        }
    }
}
public enum EventType
{
    PaltformPublished,
    Undetermined
}
public enum EventTypeWebService
{
    Platform_Published
}