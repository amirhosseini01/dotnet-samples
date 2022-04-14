using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;
        public PlatformsController(IMapper mapper, IPlatformRepo repository, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _mapper = mapper;
            _repository = repository;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Debug.WriteLine("--> Getting Platforms");

            var platformItem = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform is null)
                return NotFound();

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody] PlatformCreateDto dto)
        {
            var model = _mapper.Map<Platform>(dto);
            _repository.CreatePlatform(model);
            _repository.SaveChanges();

            var paltformReadDto = _mapper.Map<PlatformReadDto>(model);

            //Send Sync Message
            try
            {
                await _commandDataClient.SendPlatformToCommand(paltformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message + " " + ex.InnerException?.Message}");
            }

            //Send Async Message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(paltformReadDto);
                platformPublishedDto.Event = nameof(EventType.Platform_Published);
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message + " " + ex.InnerException?.Message}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = model.Id }, paltformReadDto);
        }
    }
    public enum EventType
    {
        Platform_Published
    }
}